using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee.Frame.Serialization
{
    public abstract class BaseFrameSerializer : IXbeeFrameSerializer
    {
        public virtual void Deserialize(IXBeeFrame frame, byte[] buffer)
        {
            // read the 64 bit hw address
            byte[] address = new byte[8];
            Array.Copy(buffer, FrameIndex.Address, address, 0, 8);
            frame.Address = address;
        }

        public static IXBeeFrame Deserialize(byte[] buffer, int frameLength)
        {
            FrameType frameType = (FrameType)buffer[FrameIndex.FrameType];

            // get the frame type from the frame, and create the correct frame instance
            IXBeeFrame frame = (IXBeeFrame)FrameFactory.CreateFrameWithType(frameType);
            IXbeeFrameSerializer serializer = (IXbeeFrameSerializer)FrameFactory.CreateFrameSerializerWithType(frameType);

            serializer.Deserialize(frame, buffer);

            return frame;
        }

        public virtual int Serialize(IXBeeFrame frame, byte[] resultArray, int offset)
        {
            int index = 0;

            // add the frame type
            resultArray[index++] = (byte)this.FrameType;

            // add the frame ID
            resultArray[index++] = 0x01;

            // first add the destination address. 64 bits.
            for (int i = 0; i < frame.Address.Length; i++)
            {
                resultArray[index++] = frame.Address[i];
            }

            // add the 16bit network address
            resultArray[index++] = 0xFF;
            resultArray[index++] = 0xFE;

            return index;
        }

        /// <summary>
        /// Will be implemented by subclasses
        /// </summary>
        public abstract FrameType FrameType { get; }
    }
}
