using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device;

namespace MosziNet.HomeAutomation.ApplicationLogic.MqttDeviceTranslator
{
    /// <summary>
    /// This interface is the bases of converting between device data and serialized messages.
    /// </summary>
    public interface IDeviceTranslator
    {
        /// <summary>
        /// Serializes the device status into a string message.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        string GetDeviceMessage(IDevice device);

        /// <summary>
        /// Processes the message sent to the device.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="message"></param>
        void ProcessMessage(IDevice device, string message);
    }
}
