using System;
using Microsoft.SPOT;
using System.Collections;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.Logging;

namespace MosziNet.HomeAutomation.Device
{
    /// <summary>
    /// Keeps an up to date registry of the devices in the system.
    /// </summary>
    public class DeviceTypeRegistry
    {
        private Hashtable deviceRegistry = new Hashtable();


        /// <summary>
        /// Registers a device into the system.
        /// </summary>
        /// <param name="device">The class type that handles all things related to a device.</param>
        /// <param name="deviceByteId">The address/id of the device.</param>
        public void RegisterDevice(Type device, byte[] deviceByteId)
        {
            string deviceId = HexConverter.ToHexString(deviceByteId);

            if (deviceRegistry.Contains(deviceId))
            {
                Log.Debug("Replacing device type for id: " + deviceId);
            }

            deviceRegistry[deviceId] = device;
        }

        /// <summary>
        /// Returns the class type that handles all things related to the device with the specified address.
        /// </summary>
        /// <param name="deviceByteId"></param>
        /// <returns></returns>
        public Type GetDeviceTypeById(byte[] deviceByteId)
        {
            string deviceId = HexConverter.ToHexString(deviceByteId);

            return deviceRegistry.Contains(deviceId) ? (Type)deviceRegistry[deviceId] : null;
        }

        
    }
}
