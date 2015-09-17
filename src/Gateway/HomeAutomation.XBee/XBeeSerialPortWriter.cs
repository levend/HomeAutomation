using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;

namespace MosziNet.HomeAutomation.XBee
{
    public static class XBeeSerialPortWriter
    {
        private static byte[] writeBuffer = new byte[XBeeConstants.MaxFrameLength];

        public static void WriteFrameToSerialPort(IXBeeFrame frame, ISerialPort port)
        {
            FrameSerializer.Serialize(frame, writeBuffer);

            int byteCount = FrameUtil.FrameTotalLength(writeBuffer);

            port.Write(writeBuffer, 0, byteCount);

            Debug.Print("Written to port: " + HexConverter.ToSpacedHexString(writeBuffer, 0, byteCount));
        }
    }
}
