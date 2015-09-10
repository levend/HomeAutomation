using System;
using Microsoft.SPOT;
using System.IO.Ports;

namespace MosziNet.HomeAutomation.XBee
{
    public class SerialPortWrapper : ISerialPort
    {
        private SerialPort serialPort;

        public SerialPortWrapper(SerialPort port)
        {
            serialPort = port;
        }

        public int BytesToRead
        {
            get
            {
                return serialPort.BytesToRead;
            }
        }

        public int ReadByte()
        {
            return serialPort.ReadByte();
        }

        public void Read(byte[] readBuffer, int p, int frameLength)
        {
            serialPort.Read(readBuffer, p, frameLength);
        }
    }
}
