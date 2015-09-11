using System;
using Microsoft.SPOT;
using System.Text;
using MosziNet.HomeAutomation.XBee.Frame;

namespace MosziNet.HomeAutomation.XBee
{
    public static class XBeeFrameSerialization
    {
        public static IXBeeFrame Deserialize(byte[] buffer, int length)
        {
            // get the frame type from the frame, and create the correct frame instance
            IXbeeFrameSerializable serializableFrame = (IXbeeFrameSerializable) FrameFactory.CreateFrameWithType((FrameType)buffer[0]);

            serializableFrame.Deserialize(buffer);
            
            return (IXBeeFrame) serializableFrame;
        }
    }
}
