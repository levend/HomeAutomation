﻿using System;
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
using MosziNet.HomeAutomation.Watchdog;
using MosziNet.HomeAutomation.Configuration;

namespace MosziNet.HomeAutomation
{
    public class Program
    {
        public static void Main()
        {
            WaitForNetworkAccess();
            InitializeSystemClock();

            InitializeApplicationConfiguration();
            InitializeApplicationServices();
        }

        private static void WaitForNetworkAccess()
        {
            while (IPAddress.GetDefaultLocalAddress() == IPAddress.Any) ;
        }

        private static void InitializeSystemClock()
        {
            MosziNet.HomeAutomation.Util.HttpDateTimeExtraction.FromGmtOffset(2).InitializeSystemClock(); 
        }

        private static void InitializeApplicationServices()
        {
            // Setup the device type registry
            DeviceTypeRegistry deviceRegistry = new DeviceTypeRegistry();

            // Set up the mqtt service
            MqttService mqttService = new MqttService(new MosziNet.HomeAutomation.Configuration.MqttServerConfiguration(
                "192.168.1.213",
                20,
                "MosziNet_HomeAutomation_Gateway_v1.1",
                "/MosziNet_HA"));

            // Setup the logging framework
            Log.AddLogWriter(new MqttLogWriter(mqttService, MqttTopic.LogTopic), new StandardLogFormatter());
            Log.AddLogWriter(new ConsoleLogWriter(), new StandardLogFormatter());

            // Setup the message bus
            IMessageBus messageBus = new MessageBus();
            IMessageBusRunner messageBusRunner = new ThreadedMessageBusRunner(messageBus, new MessageProcessorRegistry());
            messageBus.MessageBusRunner = messageBusRunner;            

            // now register all services
            XBeeService xbeeService = new XBeeService();

            // Register all services to the service registry
            ApplicationContext.ServiceRegistry.RegisterService(typeof(DeviceTypeRegistry), deviceRegistry);
            ApplicationContext.ServiceRegistry.RegisterService(typeof(IMessageBus), messageBus);

            ApplicationContext.ServiceRegistry.RegisterService(typeof(XBeeService), xbeeService);
            ApplicationContext.ServiceRegistry.RegisterService(typeof(MqttService), mqttService);

            ApplicationContext.ServiceRegistry.RegisterService(typeof(Gateway), new Gateway(xbeeService, mqttService));

            ApplicationContext.ServiceRegistry.RegisterService(typeof(WatchdogService), new WatchdogService(messageBus, mqttService));

            Log.Debug("[MosziNet_HA] Starting up...");

            // Start listening for messages
            xbeeService.StartListeningForMessages();
            mqttService.StartMqttClientWatchdog();
        }

        private static void InitializeApplicationConfiguration()
        {
            ApplicationConfiguration.RegisterValueForKey(ApplicationConfigurationCategory.DeviceTypeID, 0x9988, typeof(MosziNet.HomeAutomation.Device.Concrete.TemperatureDeviceV1));
            ApplicationConfiguration.RegisterValueForKey(ApplicationConfigurationCategory.DeviceTypeID, 0x9987, typeof(MosziNet.HomeAutomation.Device.Concrete.TemperatureDeviceV1));
        }

    }
}
