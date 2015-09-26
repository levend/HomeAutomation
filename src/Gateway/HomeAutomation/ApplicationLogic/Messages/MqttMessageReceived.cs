using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Messaging;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation.Admin;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.Logging;

namespace MosziNet.HomeAutomation.ApplicationLogic.Messages
{
    /// <summary>
    /// This message carries information about a command that is to be executed in the system.
    /// By convention if the address of this command is 0, then the command needs to be executed by the gateway,
    /// otherwise it will be executed by the device that was targeted by that address.
    /// </summary>
    public class MqttMessageReceived : IProcessableMessage
    {
        private string topic;
        private string message;

        /// <summary>
        /// Builds a new message. 
        /// </summary>
        /// <param name="topicName"></param>
        /// <param name="messageContent"></param>
        public MqttMessageReceived(string topicName, string messageContent)
        {
            topic = topicName;
            message = messageContent;
        }

        /// <summary>
        /// Processes the message, executes the command.
        /// </summary>
        public void ProcessMessage()
        {
            // if the message was coming through the /Admin topic then execute it with the AdminCommandDistributor
            if (topic == MqttTopic.Admin)
            {
                AdminCommandDistributor.ExecuteCommand(message);
            }

            // if the message arrived through the /Command topic then send the command directly to the device that was targeted
            if (topic == MqttTopic.CommandTopic)
            {
                DeviceCommand command = DeviceCommand.CreateFromString(message);
                DeviceRegistry deviceRegistry = (DeviceRegistry)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(DeviceRegistry));

                IDevice device = deviceRegistry.GetDeviceById(command.DeviceID) as IDevice;
                if (device != null)
                {
                    device.ExecuteCommand(command);
                }
                else
                {
                    Log.Error("A command was sent to an unknown device with ID " + HexConverter.ToHexString(command.DeviceID));
                }
            }
        }
    }
}
