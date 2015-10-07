using MosziNet.HomeAutomation.XBee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosziNet.HomeAutomation.NetCore.RPI
{
    /// <summary>
    /// Implements the <see cref="ISerialPort"/> needed for XBee serial communication.
    /// </summary>
    public class XBeeSerialPort : ISerialPort
    {
        public int BytesToRead
        {
            get
            {
                throw new NotImplementedException();
            }
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
    }
}
