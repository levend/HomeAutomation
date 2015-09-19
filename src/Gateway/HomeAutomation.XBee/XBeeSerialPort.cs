using System;
using Microsoft.SPOT;
using System.IO.Ports;
using System.Threading;

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
            WaitForBytesCountToBeAvailable(1);

            return serialPort.ReadByte();
        }

        public void Read(byte[] readBuffer, int destinationOffset, int frameLength)
        {
            WaitForBytesCountToBeAvailable(frameLength);

            serialPort.Read(readBuffer, destinationOffset, frameLength);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            serialPort.Write(buffer, offset, count);
        }

        public void Dispose()
        {
            serialPort.Close();
            serialPort.Dispose();
        }


        private void WaitForBytesCountToBeAvailable(int count)
        {
            // wait until we can actually read that many bytes.
            while (count > serialPort.BytesToRead)
            {
                Thread.Sleep(10);
            }
        }

    }
}
