using MosziNet.XBee;
using System;
using System.Collections.Generic;

namespace HomeAutomation.Tests.IntegrationTests
{
    class MockXBeeSerialPort : IXBeeSerialPort
    {
        Queue<byte[]> frameList = new Queue<byte[]>();

        List<byte[]> framesWritten = new List<byte[]>();

        public bool SerialPortConnected
        {
            get
            {
                return true;
            }
        }

        public event EventHandler<byte[]> FrameWritten;
        public event EventHandler<EventArgs> SerialPortConnectionChanged;

        public byte[] GetNextAvailableFrame()
        {
            if (frameList.Count > 0)
                return frameList.Dequeue();
            else
                return null;
        }

        public void WriteFrame(byte[] frame)
        {
            FrameWritten?.Invoke(this, frame);

            framesWritten.Add(frame);
        }

        public void EnqueueFrame(byte[] frame)
        {
            frameList.Enqueue(frame);
        }

        public List<byte[]> FlushWritternFrames()
        {
            List<byte[]> returnList = new List<byte[]>(framesWritten);

            framesWritten.Clear();

            return returnList;
        }
    }
}
