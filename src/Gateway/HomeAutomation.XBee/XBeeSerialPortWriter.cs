using System;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using MosziNet.HomeAutomation.Logging;

namespace MosziNet.HomeAutomation.XBee
{
    public static class XBeeSerialPortWriter
    {
        private static byte[] writeBuffer = new byte[XBeeConstants.MaxFrameLength];

        public static void WriteFrameToSerialPort(IXBeeFrame frame, IXBeeSerialPort port)
        {
            FrameSerializer.Serialize(frame, writeBuffer);

            int byteCount = FrameUtil.FrameTotalLength(writeBuffer);

            // copy just the bytes needed to be sent
            byte[] bytesToWrite = new byte[byteCount];
            Array.Copy(writeBuffer, bytesToWrite, byteCount);

            // statistics counting
            XBeeStatistics.MessagesSent++;

            port.WriteFrame(bytesToWrite);

            Log.Debug("[XBeeSerialPortWriter] Frame sent: " + HexConverter.ToSpacedHexString(writeBuffer, 0, byteCount));
        }
    }
}
