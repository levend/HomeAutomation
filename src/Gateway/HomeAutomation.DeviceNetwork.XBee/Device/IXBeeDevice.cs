using HomeAutomation.Core;
using MosziNet.XBee.Frame;

namespace HomeAutomation.DeviceNetwork.XBee.Device
{
    public interface IXBeeDevice : IDevice
    {
        void ProcessFrame(IXBeeFrame frame);
    }
}
