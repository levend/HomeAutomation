using System;
using Microsoft.SPOT;
using System.IO.Ports;

namespace MosziNet.HomeAutomation.XBee
{
    public class SerialPortWrapper : ISerialPort
    {
        private SerialPort serialPort;

        public SerialPortWrapper(string portName, int baudRate, Parity parity, int dataBits, System.IO.Ports.StopBits stopBits)
        {
            serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            
            serialPort.Open(); // Todo: when to close it ? Disposable.
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
