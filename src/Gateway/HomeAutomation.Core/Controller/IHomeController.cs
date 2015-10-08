using System;

namespace HomeAutomation.Core
{
    /// <summary>
    /// The IHomeController interface is implemented by every controller participating in the system.
    /// </summary>
    public interface IHomeController
    {
        event EventHandler<DeviceCommand> DeviceCommandArrived;

        /// <summary>
        /// Send a device state update to the controller.
        /// </summary>
        /// <param name="deviceState"></param>
        void SendDeviceState(DeviceState deviceState);
    }
}
