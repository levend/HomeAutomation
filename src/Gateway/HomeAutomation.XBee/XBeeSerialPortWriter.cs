using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.XBee
{
    public static class XBeeSerialPortWriter
    {
        private static byte[] writeBuffer = new byte[XBeeConstants.MaxFrameLength];

        public static void WriteFrameToSerialPort(IXBeeFrame frame, ISerialPort port)
        {
            return;
            // todo

            //IXbeeFrameSerializer serializer = FrameFactory.CreateFrameSerializerWithType(frame.FrameType);

            //int byteCount = serializer.Serialize(frame, writeBuffer, 0);

            //port.Write(writeBuffer, 0, byteCount);

            //Debug.Print("Written to port: " + HexConverter.ToSpacedHexString(writeBuffer, 0, byteCount));
        }
    }
}
