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
    public class DeviceRegistry
    {
        private Hashtable deviceRegistry = new Hashtable();
        private ArrayList stagingDevices = new ArrayList();

        /// <summary>
        /// Registers a device into the system.
        /// </summary>
        /// <param name="device">The device instance that handles all things related to a device.</param>
        /// <param name="deviceByteId">The address/id of the device.</param>
        public void RegisterDevice(IDevice device, byte[] deviceByteId)
        {
            string deviceId = HexConverter.ToHexString(deviceByteId);

            if (deviceRegistry.Contains(deviceId))
            {
                Log.Debug("Replacing device for id: " + deviceId);
            }

            deviceRegistry[deviceId] = device;
            stagingDevices.Remove(deviceId);
        }

        /// <summary>
        /// Returns the class type that handles all things related to the device with the specified address.
        /// </summary>
        /// <param name="deviceByteId"></param>
        /// <returns></returns>
        public IDevice GetDeviceById(byte[] deviceByteId)
        {
            string deviceId = HexConverter.ToHexString(deviceByteId);

            IDevice device = deviceRegistry.Contains(deviceId) ? (IDevice)deviceRegistry[deviceId] : null;
            if (device != null)
            {
                device.DeviceID = deviceByteId;
            }

            return device;
        }

        public void RegisterStagingDevice(byte[] deviceByteId)
        {
            string deviceId = HexConverter.ToHexString(deviceByteId);

            stagingDevices.Add(deviceId);
        }

        public bool IsStagingDevice(byte[] deviceByteId)
        {
            string deviceId = HexConverter.ToHexString(deviceByteId);

            return stagingDevices.Contains(deviceId);
        }        
    }
}