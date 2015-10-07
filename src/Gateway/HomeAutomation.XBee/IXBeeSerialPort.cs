using System;

namespace MosziNet.HomeAutomation.XBee
{
    public interface IXBeeSerialPort
    {
        byte[] GetNextAvailableFrame();

        void WriteFrame(byte[] frame);
    }
}
