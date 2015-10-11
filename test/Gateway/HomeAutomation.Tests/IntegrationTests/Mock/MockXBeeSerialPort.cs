using System;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.XBee.Frame;

namespace HomeAutomation.Tests.IntegrationTests
{
    class MockXBeeSerialPort : IXBeeSerialPort
    {
        public class XBeeFrameRequest
        {
            public byte[] Frame { get; set; }
        }

        public event EventHandler<XBeeFrameRequest> NewFrameRequired;
        public event EventHandler<byte[]> FrameWritten;

        public byte[] GetNextAvailableFrame()
        {
            XBeeFrameRequest xfr = new XBeeFrameRequest();

            NewFrameRequired?.Invoke(this, xfr);

            return xfr.Frame;
        }

        public void WriteFrame(byte[] frame)
        {
            FrameWritten?.Invoke(this, frame);
        }
    }
}
