using HomeAutomation.Core.Service;
using System;

namespace HomeAutomation.Core
{
    /// <summary>
    /// The IDeviceNetwork interface defines the means on how to connect new device networks to the system. 
    /// A device network is a grouping of the devices based on how they communicate - eg. XBee device network, nRF device network, etc ...
    /// </summary>
    public interface IDeviceNetwork
    {
        /// <summary>
        /// The event is fired when the device network delivers a message to the controller.
        /// </summary>
        event EventHandler<DeviceState> DeviceStateReceived;

        /// <summary>
        /// Sends a command to the device network.
        /// </summary>
        /// <param name="command"></param>
        void SendCommand(DeviceCommand command);

        /// <summary>
        /// Gets/sets the name of the device network. Set by the system when the network is registered.
        /// </summary>
        string Name { get; set; }
    }
}
