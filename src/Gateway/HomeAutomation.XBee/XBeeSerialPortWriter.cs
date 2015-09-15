using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame;

namespace MosziNet.HomeAutomation.XBee
{
    public static class XBeeSerialPortWriter
    {
        private static byte[] writeBuffer = new byte[XBeeConstants.MaxFrameLength];

        public static void WriteFrameToSerialPort(IXBeeFrame frame, ISerialPort port)
        {
            IXbeeFrameSerializer serializer = FrameFactory.CreateFrameSerializerWithType(frame.FrameType);

            serializer.Serialize(frame, writeBuffer, 0);
        }
    }
}
