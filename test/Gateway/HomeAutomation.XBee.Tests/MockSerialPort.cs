using MosziNet.HomeAutomation.XBee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.XBee.Tests
{
    public class MockSerialPort : IXBeeSerialPort
    {
        private byte[] values;

        public MockSerialPort(byte[] valuesToRead)
        {
            values = valuesToRead;
        }

        public byte[] GetNextAvailableFrame()
        {
            return values;
        }

        public void WriteFrame(byte[] frame)
        {
            
        }
    }
}
