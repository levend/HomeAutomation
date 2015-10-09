using HomeAutomation.Core;

namespace MosziNet.HomeAutomation.Gateway.BusinessLogic
{
    public class DeviceNetworkGateway
    {
        public DeviceNetworkGateway()
        {
            // make sure we subscribe to new networks' events ...
            HomeAutomationSystem.DeviceNetworkRegistry.DeviceNetworkAdded += (sender, network) =>
            {
                network.DeviceStateReceived += DeviceNetwork_DeviceStateReceived;
            };

            HomeAutomationSystem.ControllerRegistry.ControllerAdded += (sender, controller) =>
            {
                controller.DeviceCommandArrived += Controller_DeviceCommandArrived;
            };
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
