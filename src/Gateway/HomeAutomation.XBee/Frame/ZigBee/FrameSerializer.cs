using System;
using System.Reflection;

namespace MosziNet.HomeAutomation.XBee.Frame.ZigBee
{
    /// <summary>
    /// Serializes and deserializes XBee frames.
    /// </summary>
    public static class FrameSerializer
    {
        private const string FrameNamespace = "MosziNet.HomeAutomation.XBee.Frame.ZigBee";

        /// <summary>
        /// Deserializes the buffer to the correct XBee frame.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static IXBeeFrame Deserialize(byte[] buffer)
        {
            IXBeeFrame newFrame = null;

            // get the frame's type and it's descriptor.
            string frameName = ((FrameType)buffer[FrameIndex.FrameType]).ToString();
            int length = buffer[FrameIndex.LengthMSB] * 256 + buffer[FrameIndex.LengthLSB];

            PropertyDescriptor[] frameDescriptor = APIv1Descriptor.GetFrameDescriptor(frameName);

            Type newFrameType = Assembly.GetExecutingAssembly().GetType(FrameNamespace + "." + frameName);
            newFrame = (IXBeeFrame)(newFrameType.GetConstructor(new Type[] { }).Invoke(new object[] { }));

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
        /// <param name="buffer"></param>
        public static void Serialize(IXBeeFrame frame, byte[] buffer)
        {
            string frameName = frame.FrameType.ToString();

            PropertyDescriptor[] frameDescriptor = APIv1Descriptor.GetFrameDescriptor(frameName);
            Type frameType = frame.GetType();
            
            // set the frame start byte.
            buffer[FrameIndex.Start] = XBeeConstants.FrameStart;

            MethodInfo getterMethod;
            int index = 0;
            for (int i = 0; i < frameDescriptor.Length; i++)
            {
                PropertyDescriptor onePropertyDescriptor = frameDescriptor[i];
                if (onePropertyDescriptor.PropertyType != PropertyType.Ignored)
                {
                    // get the setter method information
                    getterMethod = frameType.GetMethod("get_" + onePropertyDescriptor.PropertyName);

                    object returnValue = getterMethod.Invoke(frame, new object[] { });
                    if (returnValue != null)
                    {
                        SetPropertyValueToBuffer(buffer, index, returnValue, onePropertyDescriptor);
                    }
                }

                index += onePropertyDescriptor.ByteCount;
            }

            buffer[index] = CalculateChecksum(buffer, index);
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
                // todo
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

        private static void SetPropertyValueToBuffer(byte[] buffer, int index, object value, PropertyDescriptor onePropertyDescriptor)
        {
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
                    Array.Copy((byte[])value, 0, buffer, index, onePropertyDescriptor.ByteCount);
                    break;
            }
        }

        private static byte CalculateChecksum(byte[] resultArray, int index)
        {
            int checksum = 0;

            for (int i = FrameIndex.FrameType; i < index; i++)
            {
                checksum += resultArray[i];
            }

            return (byte)(0xFF - (checksum & 0xFF));
        }


        #endregion
    }
}
