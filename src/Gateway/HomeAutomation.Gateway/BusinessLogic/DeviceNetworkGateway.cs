using HomeAutomation.Core;
using HomeAutomation.Core.Service;

namespace HomeAutomation.Gateway.BusinessLogic
{
    public class DeviceNetworkGateway : IService
    {
        public DeviceNetworkGateway()
        {
            // make sure we subscribe to new networks' events ...
            HomeAutomationSystem.DeviceNetworkRegistry.DeviceNetworkAdded += (sender, network) =>
            {
                network.DeviceStateReceived += DeviceNetwork_DeviceStateReceived;
            };

            // the controllers are aggregated with the "All" controller, subscribe to the commands there.
            HomeAutomationSystem.ControllerRegistry.All.DeviceCommandArrived += Controller_DeviceCommandArrived;
        }

        private void Controller_DeviceCommandArrived(object sender, DeviceCommand e)
        {
            IDeviceNetwork deviceNetwork = HomeAutomationSystem.DeviceNetworkRegistry.GetNetworkByName(e.DeviceNetworkName);
            deviceNetwork?.SendCommand(e);
        }

        private void DeviceNetwork_DeviceStateReceived(object sender, DeviceState e)
        {
            HomeAutomationSystem.ControllerRegistry.All.SendDeviceState(e);
        }
    }
}
