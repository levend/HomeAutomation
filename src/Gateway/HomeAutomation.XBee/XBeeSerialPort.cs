using System;
using Microsoft.SPOT;
using System.IO.Ports;

namespace MosziNet.HomeAutomation.XBee
{
    public class XBeeSerialPort : ISerialPort, IDisposable
    {
        private SerialPort serialPort;

        public XBeeSerialPort(string portName, int baudRate, Parity parity, int dataBits, System.IO.Ports.StopBits stopBits)
        {
            serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            
            serialPort.Open();
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

        public void Read(byte[] readBuffer, int destinationOffset, int frameLength)
        {
            serialPort.Read(readBuffer, destinationOffset, frameLength);
        }

        public void Dispose()
        {
            serialPort.Close();
            serialPort.Dispose();
        }
    }
}
