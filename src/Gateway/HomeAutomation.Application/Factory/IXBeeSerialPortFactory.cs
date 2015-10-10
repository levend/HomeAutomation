using HomeAutomation.Application.Configuration;
using MosziNet.HomeAutomation.XBee;

namespace HomeAutomation.Application.Factory
{
    public interface IXBeeSerialPortFactory
    {
        IXBeeSerialPort Create(XBeeConfiguration config);
    }
}
