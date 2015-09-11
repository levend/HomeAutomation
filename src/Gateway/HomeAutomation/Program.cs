using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation;
using MosziNet.HomeAutomation.CommunicationService;
using System.IO.Ports;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.Sensor.Temperature;

namespace MosziNet.HomeAutomation
{
    public class Program
    {
        public static void Main()
        {
            InitializeApplication();
        }

        private static void InitializeApplication()
        {
            // Setup the device registry
            // TODO: either provide the means to register devices through MQTT, or just register them statically ? Do we really need this in a gateway (which should just pass over the messages) ?
            DeviceRegistry deviceRegistry = new DeviceRegistry();

            // Setup the communication service providers
            CommunicationServiceProvider communicationServiceProvider = new CommunicationServiceProvider();
            communicationServiceProvider.RegisterCommunicationService(new XBeeCommunicationService(), ApplicationsConstants.CommunicationServiceProvider_XBee);
            //communicationServiceProvider.RegisterCommunicationService(
            //    new MqttCommunicationService(new MosziNet.HomeAutomation.Configuration.MqttServerConfiguration(
            //        "192.168.1.213", 
            //        20, 
            //        "MosziNet_HomeAutomation_Gateway",
            //        "/MosziNet_HA/")), 
            //    ApplicationsConstants.CommunicationServiceProvider_MQTT);

            // Setup the message bus            
            IMessageBus messageBus = new MessageBus();

            MessageProcessorRegistry messageProcessorRegistry = new MessageProcessorRegistry();
            messageProcessorRegistry.RegisterMessageProcessor(ApplicationsConstants.MessageType_XBeeDeviceCommand, new XBeeDeviceCommandMessageProcessor());
            IMessageBusRunner messageBusRunner = new ThreadedMessageBusRunner(messageBus, messageProcessorRegistry);
            messageBus.MessageBusRunner = messageBusRunner;            

            // now register all services
            ApplicationContext.ServiceRegistry.RegisterService(typeof(IMessageBus), messageBus);
            ApplicationContext.ServiceRegistry.RegisterService(typeof(IDeviceRegistry), deviceRegistry);
            ApplicationContext.ServiceRegistry.RegisterService(typeof(ICommunicationServiceProvider), communicationServiceProvider);
        }
    }
}
