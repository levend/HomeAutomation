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
using System.IO.Ports;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.Sensor.Temperature;
using MosziNet.HomeAutomation.BusinessLogic;
using MosziNet.HomeAutomation.ApplicationLogic.Messages;
using MosziNet.HomeAutomation.Mqtt;
using MosziNet.HomeAutomation.Logging.Writer;
using MosziNet.HomeAutomation.Logging.Formatter;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Messaging;

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
            MosziNet.HomeAutomation.Util.HttpDateTimeExtraction.FromGmtOffset(2).InitializeSystemClock();

            // Setup the device registry
            // TODO: either provide the means to register devices through MQTT, or just register them statically ? Do we really need this in a gateway (which should just pass over the messages) ?
            DeviceTypeRegistry deviceRegistry = new DeviceTypeRegistry();

            MqttService mqttService = new MqttService(new MosziNet.HomeAutomation.Configuration.MqttServerConfiguration(
                "192.168.1.213",
                20,
                "MosziNet_HomeAutomation_Gateway_v1.1",
                "/MosziNet_HA"));

            // setup the logging framework
            Log.AddLogWriter(new MqttLogWriter(mqttService, "/Moszinet_HA/Log"), new StandardLogFormatter());
            Log.AddLogWriter(new ConsoleLogWriter(), new StandardLogFormatter());

            // Setup the message bus
            IMessageBus messageBus = new MessageBus();

            MessageProcessorRegistry messageProcessorRegistry = new MessageProcessorRegistry();
            IMessageBusRunner messageBusRunner = new ThreadedMessageBusRunner(messageBus, messageProcessorRegistry);
            messageBus.MessageBusRunner = messageBusRunner;            

            // now register all services
            XBeeService xbeeService = new XBeeService();

            ApplicationContext.ServiceRegistry.RegisterService(typeof(XBeeService), xbeeService);
            ApplicationContext.ServiceRegistry.RegisterService(typeof(IMessageBus), messageBus);
            ApplicationContext.ServiceRegistry.RegisterService(typeof(IDeviceTypeRegistry), deviceRegistry);
            ApplicationContext.ServiceRegistry.RegisterService(typeof(Gateway), new Gateway());
            ApplicationContext.ServiceRegistry.RegisterService(typeof(MqttService), mqttService);

            xbeeService.StartListeningForMessages();
        }
    }
}
