using System;

namespace MosziNet.HomeAutomation.Device
{
    public interface IDeviceTypeRegistry
    {
        Type GetDeviceTypeById(byte[] deviceId);

        void RegisterDevice(Type deviceType, byte[] deviceId);
    }
}
