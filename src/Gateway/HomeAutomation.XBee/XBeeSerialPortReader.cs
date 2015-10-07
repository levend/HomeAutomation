using System;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using System.Threading;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.XBee
{
    public static class XBeeSerialPortReader
    {
        public static IXBeeFrame FrameFromSerialPort(IXBeeSerialPort port)
        {
            IXBeeFrame frame = null;

            byte[] frameBytes = port.GetNextAvailableFrame();
            if (frameBytes != null)
            {
                // now create an XBee frame based on the buffer
                frame = FrameSerializer.Deserialize(frameBytes);

                // Log the frame ...
                Log.Debug("[XBeeSerialPortReader] Frame received: " + HexConverter.ToSpacedHexString(frameBytes, 0, frameBytes.Length));

                // statistics counting
                XBeeStatistics.MessagesReceived++;
            }

            return frame;
        }
    }
}
