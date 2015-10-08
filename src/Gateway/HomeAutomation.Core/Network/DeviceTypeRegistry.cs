using System;
using System.Collections.Generic;

namespace HomeAutomation.Core
{
    /// <summary>
    /// Device networks should register here the device types they handle.
    /// </summary>
    public class DeviceTypeRegistry
    {
        private Dictionary<string, Type> deviceTypes = new Dictionary<string, Type>();

        /// <summary>
        /// Registers all device types from the given network.
        /// </summary>
        /// <param name="deviceNetwork"></param>
        public void RegisterDeviceTypes(IDeviceNetwork deviceNetwork)
        {
            IEnumerable<DeviceTypeDescription> deviceTypeList = deviceNetwork.AvailableDeviceTypes;

            string networkId = HomeAutomationSystem.DeviceNetworkRegistry.GetDeviceNetworkUniqueId(deviceNetwork);

            foreach(DeviceTypeDescription aType in deviceTypeList)
            {
                deviceTypes.Add($"{networkId}-{aType.DeviceTypeId}", aType.DeviceClassType);
            }
        }

        /// <summary>
        /// Registers the given device type.
        /// </summary>
        /// <param name="deviceNetwork"></param>
        /// <param name="aType"></param>
        public void RegisterDeviceType(IDeviceNetwork deviceNetwork, DeviceTypeDescription aType)
        {
            string networkId = HomeAutomationSystem.DeviceNetworkRegistry.GetDeviceNetworkUniqueId(deviceNetwork);

            deviceTypes.Add($"{networkId}-{aType.DeviceTypeId}", aType.DeviceClassType);
        }

        /// <summary>
        /// Returns the device type by it's typeId.
        /// </summary>
        /// <param name="deviceNetwork"></param>
        /// <param name="deviceTypeId"></param>
        /// <returns></returns>
        public Type GetDeviceType(IDeviceNetwork deviceNetwork, int deviceTypeId)
        {
            string networkId = HomeAutomationSystem.DeviceNetworkRegistry.GetDeviceNetworkUniqueId(deviceNetwork);

            string key = $"{networkId}-{deviceTypeId}";

            return deviceTypes.ContainsKey(key) ? deviceTypes[key] : null;
        }
    }
}
