using System;
using System.Collections.Generic;

namespace HomeAutomation.Core
{
    /// <summary>
    /// The IHomeController interface is implemented by every controller participating in the system.
    /// </summary>
    public interface IController
    {
        event EventHandler<DeviceCommand> DeviceCommandArrived;

        /// <summary>
        /// Send a device state update to the controller.
        /// </summary>
        /// <param name="deviceState"></param>
        void SendDeviceState(DeviceState deviceState);

        /// <summary>
        /// Sends a heartbeat message to the home controllers so they know that the gateway is alive.
        /// </summary>
        /// <param name="message"></param>
        void SendGatewayHeartbeatMessage(string message);

        /// <summary>
        /// Sends statistics about the system usage.
        /// </summary>
        /// <param name="statisticValues"></param>
        void SendStatistics(Dictionary<string, object> statisticValues);
    }
}
