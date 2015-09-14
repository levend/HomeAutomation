using System;

namespace MosziNet.HomeAutomation.Device
{
    public interface IDeviceTypeRegistry
    {
        Type GetDeviceTypeById(string deviceId);

        void RegisterDevice(Type deviceType, string deviceId);
    }
}
