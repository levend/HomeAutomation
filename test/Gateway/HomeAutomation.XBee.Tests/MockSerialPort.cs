using MosziNet.HomeAutomation.XBee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.XBee.Tests
{
    public class MockSerialPort : ISerialPort
    {
        private byte[] values;
        private int index;

        public MockSerialPort(byte[] valuesToRead)
        {
            values = valuesToRead;
        }

        public int BytesToRead
        {
            get
            {
                return values.Length - index;
            }
        }

        public int ReadByte()
        {
            return values[index++];
        }

        public void Read(byte[] readBuffer, int destinationOffset, int frameLength)
        {

            for(int i = 0; i < frameLength; i++)
            {
                readBuffer[destinationOffset + i] = values[index++];
            }
        }

        public void Write(byte[] buffer, int offset, int count)
        {

        }
    }
}
