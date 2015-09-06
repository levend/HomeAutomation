using System;

namespace MosziNet.HomeAutomation.Device
{
    public interface IDeviceRegistry
    {
        IDevice GetDeviceByID(string deviceID);
        void RegisterDevice(IDevice device);
    }
}
