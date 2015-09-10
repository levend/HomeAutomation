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
        int index;

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

        public void Read(byte[] readBuffer, int offset, int frameLength)
        {
            for(int i = index + offset; i < values.Length; i++)
            {
                readBuffer[i - (index + offset)] = values[i];
            }

            index += offset + frameLength;
        }
    }
}
