using System;

namespace HomeAutomation.Core.Controller
{
    /// <summary>
    /// Provides simple means to aggregate controllers in the network.
    /// </summary>
    internal class AllControllersController : IHomeController
    {
        private ControllerRegistry registry;

        public event EventHandler<DeviceCommand> DeviceCommandArrived;

        public AllControllersController(ControllerRegistry registry)
        {
            this.registry = registry;

            registry.ControllerAdded += Registry_ControllerAdded;
        }

        private void Registry_ControllerAdded(object sender, IHomeController controller)
        {
            if (controller != this)
            {
                // subscribe to all events from all controllers
                controller.DeviceCommandArrived += Controller_DeviceCommandArrived;
            }
        }

        private void Controller_DeviceCommandArrived(object sender, DeviceCommand e)
        {
            // distribute all events
            DeviceCommandArrived?.Invoke(sender, e);
        }

        public void SendDeviceState(DeviceState deviceState)
        {
            IHomeController[] allControllers = registry.GetControllers();
            foreach(IHomeController oneController in allControllers)
            {
                oneController.SendDeviceState(deviceState);
            }
        }

        public void SendGatewayHeartbeatMessage(string message)
        {
            IHomeController[] allControllers = registry.GetControllers();
            foreach (IHomeController oneController in allControllers)
            {
                oneController.SendGatewayHeartbeatMessage(message);
            }
        }
    }
}
