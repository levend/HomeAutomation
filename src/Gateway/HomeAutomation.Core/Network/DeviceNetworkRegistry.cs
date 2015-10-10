using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.Core
{
    /// <summary>
    /// Keeps a register of the available device networks.
    /// </summary>
    public class DeviceNetworkRegistry
    {
        public event EventHandler<IDeviceNetwork> DeviceNetworkAdded;

        private Dictionary<IDeviceNetwork, string> deviceNetworks = new Dictionary<IDeviceNetwork, string>();
        private Dictionary<string, IDeviceNetwork> deviceNetworksByName = new Dictionary<string, IDeviceNetwork>();

        public void RegisterDeviceNetwork(IDeviceNetwork deviceNetwork, string deviceNetworkUniqueId)
        {
            deviceNetworks.Add(deviceNetwork, deviceNetworkUniqueId);
            deviceNetworksByName.Add(deviceNetworkUniqueId, deviceNetwork);

            HomeAutomationSystem.DeviceTypeRegistry.RegisterDeviceTypes(deviceNetwork);

            // Notify our listeners that a new network was added.
            DeviceNetworkAdded?.Invoke(this, deviceNetwork);
        }

        public string GetDeviceNetworkUniqueId(IDeviceNetwork network)
        {
            return deviceNetworks[network];
        }

        public IDeviceNetwork GetNetworkByName(string deviceNetworkName)
        {
            return deviceNetworksByName[deviceNetworkName];
        }

        public IDeviceNetwork[] GetDeviceNetworks()
        {
            return new List<IDeviceNetwork>(deviceNetworks.Keys).ToArray();
        }
    }
}
