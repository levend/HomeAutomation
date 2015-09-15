using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee
{
    public interface ISerialPort
    {
        int BytesToRead { get; }

        int ReadByte();

        void Read(byte[] readBuffer, int p, int frameLength);

        void Write(byte[] buffer, int offset, int count);
    }
}
