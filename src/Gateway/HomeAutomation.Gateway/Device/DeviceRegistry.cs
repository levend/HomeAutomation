using System;
using System.Collections;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Configuration;
using MosziNet.HomeAutomation.Device.Concrete;

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
            string deviceId = deviceByteId.ToHexString();

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
            string deviceId = deviceByteId.ToHexString();

            IDevice device = deviceRegistry.Contains(deviceId) ? (IDevice)deviceRegistry[deviceId] : null;

            return device;
        }

        public void RegisterStagingDevice(byte[] deviceByteId)
        {
            string deviceId = deviceByteId.ToHexString();

            stagingDevices.Add(deviceId);
        }

        public bool IsStagingDevice(byte[] deviceByteId)
        {
            string deviceId = deviceByteId.ToHexString();

            return stagingDevices.Contains(deviceId);
        }

        public void RegisterDeviceWithTypeID(int deviceIdentification, byte[] address, byte[] networkAddress)
        {
            if (IsStagingDevice(address))
            {
                // get the device from the configuration based on this type id
                Type deviceType = ApplicationContext.Configuration.GetTypeForKey(ApplicationConfigurationCategory.DeviceTypeID, deviceIdentification);
                IDevice device = Activator.CreateInstance(deviceType) as IDevice;

                if (device != null)
                {
                    device.DeviceID = address;
                    device.NetworkAddress = networkAddress;

                    RegisterDevice(device, device.DeviceID);
                }
                else
                {
                    Log.Debug($"The device type specified by the sensor with ID {address.ToHexString()} is not know.");

                    RegisterDevice(new UnknownDevice(), address);
                }
            }

        }
    }
}
