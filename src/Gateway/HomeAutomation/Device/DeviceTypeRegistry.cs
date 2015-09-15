using System;
using Microsoft.SPOT;
using System.Collections;

namespace MosziNet.HomeAutomation.Device
{
    /// <summary>
    /// Keeps an up to date registry of the devices in the system.
    /// </summary>
    public class DeviceTypeRegistry : MosziNet.HomeAutomation.Device.IDeviceTypeRegistry
    {
        private Hashtable deviceRegistry = new Hashtable();

        public void RegisterDevice(Type device, string deviceId)
        {
            deviceRegistry.Add(device, deviceId);
        }

        public Type GetDeviceTypeById(string deviceId)
        {
            return deviceRegistry.Contains(deviceId) ? (Type)deviceRegistry[deviceId] : null;
        }
    }
}
