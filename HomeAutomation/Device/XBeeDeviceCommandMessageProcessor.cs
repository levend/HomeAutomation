using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation;
using MosziNet.HomeAutomation.CommunicationService;

namespace MosziNet.HomeAutomation.Device
{
    public class XBeeDeviceCommandMessageProcessor : IMessageProcessor
    {
        public void ProcessMessage(Message message)
        {
            DeviceCommand command = GetCommandFromMessage(message.MessageContent);

            ICommunicationServiceProvider provider = (ICommunicationServiceProvider)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(ICommunicationServiceProvider));
            ICommunicationService xbeeCommunicationService = provider.GetService(ApplicationConfiguration.CommunicationServiceProvider_XBee);

            // TODO: understand how XBee handles messages, implement this properly
            xbeeCommunicationService.SendMessage(command.DeviceId, command.Command);
        }

        private DeviceCommand GetCommandFromMessage(string message)
        {
            string[] messageParts = message.Split(',');

            if (messageParts.Length == 3)
            {
                return new DeviceCommand()
                {
                    DeviceId = messageParts[0],
                    SensorId = messageParts[1],
                    Command = messageParts[2]
                };
            }
            else
            {
                Debug.Print("Device command not understood. Command message: " + message);

                return null;
            }
        }
    }
}
