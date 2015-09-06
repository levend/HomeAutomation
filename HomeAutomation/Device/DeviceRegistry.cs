using System;
using Microsoft.SPOT;
using System.Collections;

namespace MosziNet.HomeAutomation.Device
{
    /// <summary>
    /// Keeps an up to date registry of the devices in the system.
    /// </summary>
    public class DeviceRegistry : MosziNet.HomeAutomation.Device.IDeviceRegistry
    {
        private Hashtable deviceRegistry = new Hashtable();

        public void RegisterDevice(IDevice device)
        {
            deviceRegistry.Add(device, device.DeviceID);
        }

        public IDevice GetDeviceByID(string deviceID)
        {
            IDevice device = deviceRegistry.Contains(deviceID) ? (IDevice)deviceRegistry[deviceID] : null;

            return device;
        }
    }
}
