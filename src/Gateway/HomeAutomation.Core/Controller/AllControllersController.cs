using HomeAutomation.Core.Diagnostics;
using System;
using System.Collections.Generic;

namespace HomeAutomation.Core.Controller
{
    /// <summary>
    /// Provides simple means to aggregate controllers in the network.
    /// </summary>
    internal class AllControllersController : IController
    {
        private ControllerRegistry registry;

        public event EventHandler<DeviceCommand> DeviceCommandArrived;

        public AllControllersController(ControllerRegistry registry)
        {
            this.registry = registry;

            registry.ControllerAdded += Registry_ControllerAdded;
        }

        private void Registry_ControllerAdded(object sender, IController controller)
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
            IController[] allControllers = registry.GetControllers();
            foreach(IController oneController in allControllers)
            {
                oneController.SendDeviceState(deviceState);
            }
        }

        public void SendGatewayHeartbeatMessage(string message)
        {
            IController[] allControllers = registry.GetControllers();
            foreach (IController oneController in allControllers)
            {
                oneController.SendGatewayHeartbeatMessage(message);
            }
        }

        public void ExecuteTasks()
        {
            // nothing to do here
        }

        public void SendStatistics(Statistics statistics)
        {
            IController[] allControllers = registry.GetControllers();
            foreach (IController oneController in allControllers)
            {
                oneController.SendStatistics(statistics);
            }
        }
    }
}
