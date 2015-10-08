using System;
using System.Reflection;

namespace MosziNet.HomeAutomation.XBee.Frame.ZigBee
{
    /// <summary>
    /// Serializes and deserializes XBee frames.
    /// </summary>
    public static class FrameSerializer
    {
        /// <summary>
        /// Deserializes the buffer to the correct XBee frame.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static IXBeeFrame Deserialize(byte[] buffer)
        {
            IXBeeFrame newFrame = null;

            // get the frame's type and it's descriptor.
            FrameType frameType = (FrameType)buffer[FrameIndex.FrameType];

            PropertyDescriptor[] frameDescriptor = FrameTypeInfo.APIV1FrameTypeInfo.GetFrameDescriptor(frameType);
            if (frameDescriptor == null)
                return newFrame;
            
            Type newFrameType = FrameTypeInfo.APIV1FrameTypeInfo.FrameTypes[frameType];
            newFrame = (IXBeeFrame)Activator.CreateInstance(newFrameType);

            int length = buffer[FrameIndex.LengthMSB] * 256 + buffer[FrameIndex.LengthLSB];

            MethodInfo setterMethod;
            int index = 0;
            for (int i = 0; i < frameDescriptor.Length; i++)
            {
                PropertyDescriptor onePropertyDescriptor = frameDescriptor[i];
                if (onePropertyDescriptor.PropertyType != PropertyType.Ignored)
                {
                    // get the setter method information
                    setterMethod = newFrameType.GetMethod("set_" + onePropertyDescriptor.PropertyName);

                    object parameters = GetPropertyValueFromBuffer(buffer, index, onePropertyDescriptor);
                    if (parameters != null)
                    {
                        // then invoke the setter method with the right parameters
                        setterMethod.Invoke(newFrame, new object[] { parameters });
                    }
                }

                index += onePropertyDescriptor.ByteCount;
            }

            return newFrame;
        }

        /// <summary>
        /// Serializes the frame to the provided buffer.
        /// </summary>
        /// <param name="frame"></param>
        public static byte[] Serialize(IXBeeFrame frame)
        {
            byte[] buffer = new byte[XBeeConstants.MaxFrameLength];

            PropertyDescriptor[] frameDescriptor = FrameTypeInfo.APIV1FrameTypeInfo.GetFrameDescriptor(frame.FrameType);
            Type frameType = frame.GetType();

            // set the frame start byte.
            buffer[FrameIndex.Start] = XBeeConstants.FrameStart;

            MethodInfo getterMethod;
            int index = 0;
            for (int i = 0; i < frameDescriptor.Length; i++)
            {
                PropertyDescriptor onePropertyDescriptor = frameDescriptor[i];
                int writtenByteCount = onePropertyDescriptor.ByteCount;

                if (onePropertyDescriptor.PropertyType != PropertyType.Ignored)
                {
                    // get the setter method information
                    getterMethod = frameType.GetMethod("get_" + onePropertyDescriptor.PropertyName);

                    object returnValue = getterMethod.Invoke(frame, new object[] { });
                    if (returnValue != null)
                    {
                        writtenByteCount = SetPropertyValueToBuffer(buffer, index, returnValue, onePropertyDescriptor);
                    }
                }

                index += writtenByteCount;
            }

            // calculate the length of the payload, and update the frame with the information
            int payloadLength = index - 3;
            buffer[FrameIndex.LengthMSB] = (byte)(payloadLength / 256);
            buffer[FrameIndex.LengthLSB] = (byte)(payloadLength % 256);

            // calculate the checksum of the payload
            buffer[index] = XBeeFrameUtil.CalculateChecksum(buffer);

            // make a new array which contains only the resulting bytes, and return that array
            byte[] resultBuffer = new byte[index + 1];
            Array.Copy(buffer, resultBuffer, index + 1);

            return resultBuffer;
        }

        #region / Deserialization related methods /

        private static object GetPropertyValueFromBuffer(byte[] buffer, int index, PropertyDescriptor onePropertyDescriptor)
        {
            switch (onePropertyDescriptor.PropertyType)
            {
                case PropertyType.Byte:
                    return buffer[index];

                case PropertyType.Integer:
                    return buffer[index] * 256 + buffer[index + 1];

                case PropertyType.ByteArray:
                    return GetByteArrayPropertyValueFromBuffer(buffer, index, onePropertyDescriptor);
            }

            return null;
        }

        private static byte[] GetByteArrayPropertyValueFromBuffer(byte[] buffer, int index, PropertyDescriptor onePropertyDescriptor)
        {
            byte[] returnValue = null;

            // if the request was to consume all bytes, then we will do it excluding the trailing checksum
            if (onePropertyDescriptor.ByteCount == 0)
            {
                int checksumIndex = XBeeFrameUtil.CalculateChecksumIndex(buffer);
                int lengthToCopy = checksumIndex - index;

                returnValue = new byte[lengthToCopy];
                Array.Copy(buffer, index, returnValue, 0, lengthToCopy);
            }
            else
            {
                returnValue = new byte[onePropertyDescriptor.ByteCount];

                Array.Copy(buffer, index, returnValue, 0, onePropertyDescriptor.ByteCount);
            }

            return returnValue;
        }

        #endregion

        #region / Serialization related methods /

        private static int SetPropertyValueToBuffer(byte[] buffer, int index, object value, PropertyDescriptor onePropertyDescriptor)
        {
            int writtenBytes = onePropertyDescriptor.ByteCount;

            switch (onePropertyDescriptor.PropertyType)
            {
                case PropertyType.Byte:
                    buffer[index] = (byte)value;
                    break;
                case PropertyType.Integer:
                    buffer[index] = (byte)((int)value / 256);
                    buffer[index + 1] = (byte)((int)value % 256);
                    break;
                case PropertyType.ByteArray:
                    if (onePropertyDescriptor.ByteCount > 0)
                    {
                        Array.Copy((byte[])value, 0, buffer, index, onePropertyDescriptor.ByteCount);
                    }
                    else
                    {
                        byte[] localValue = (byte[])value;
                        Array.Copy(localValue, 0, buffer, index, localValue.Length);

                        writtenBytes = localValue.Length;
                    }

                    break;
            }

            return writtenBytes;
        }

        #endregion
    }
}
