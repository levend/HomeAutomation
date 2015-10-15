using HomeAutomation.Application.Configuration;
using MosziNet.XBee;

namespace HomeAutomation.Application.Factory
{
    public interface IXBeeSerialPortFactory
    {
        IXBeeSerialPort Create(XBeeConfiguration config);
    }
}
