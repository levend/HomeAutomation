using System;
using System.Threading;

namespace MosziNet.HomeAutomation.XBee
{
    public class XBeeSerialPort : ISerialPort, IDisposable
    {
        public int BytesToRead
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Read(byte[] readBuffer, int p, int frameLength)
        {
            throw new NotImplementedException();
        }

        public int ReadByte()
        {
            throw new NotImplementedException();
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        private void WaitForBytesCountToBeAvailable(int count)
        {
            // wait until we can actually read that many bytes.
            //while (count > serialPort.BytesToRead) ;
        }

    }
}
