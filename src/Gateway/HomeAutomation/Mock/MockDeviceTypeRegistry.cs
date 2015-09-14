using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation.Device.Concrete;

namespace MosziNet.HomeAutomation.Mock
{
    public class MockDeviceTypeRegistry : IDeviceTypeRegistry
    {
        public Type GetDeviceTypeById(string deviceId)
        {
            return typeof(TemperatureSensor);
        }

        public void RegisterDevice(Type deviceType, string deviceId)
        {
            
        }
    }
}
