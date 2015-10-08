using HomeAutomation.Core;
using MosziNet.HomeAutomation.XBee.Frame;

namespace HomeAutomation.DeviceNetwork.XBee.Device
{
    public interface IXBeeDevice : IDevice
    {
        void ProcessFrame(IXBeeFrame frame);
    }
}
