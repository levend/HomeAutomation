using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation;
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
using MosziNet.HomeAutomation.Device.Concrete;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using MosziNet.HomeAutomation.ApplicationLogic.XBeeFrameProcessor;
using MosziNet.HomeAutomation.Admin;

namespace MosziNet.HomeAutomation
{
    public class Program
    {
        public static void Main()
        {
            InitializeApplicationConfiguration();
            InitializeApplicationServices();
        }

        private static void InitializeApplicationServices()
        {
            ApplicationContext.ServiceRegistry = new Service.ServiceRegistry();

            RunLoop mainRunLoop = new RunLoop();

            // Setup the device type registry
            DeviceRegistry deviceRegistry = new DeviceRegistry();

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
            ThreadedMessageBusRunner messageBusRunner = new ThreadedMessageBusRunner(messageBus, new MessageProcessorRegistry());
            messageBus.MessageBusRunner = messageBusRunner;            

            // now register all services
            IXBeeService xbeeService = new XBeeService();

            // Register all services to the service registry
            ApplicationContext.ServiceRegistry.RegisterService(typeof(DeviceRegistry), deviceRegistry);
            ApplicationContext.ServiceRegistry.RegisterService(typeof(IMessageBus), messageBus);

            ApplicationContext.ServiceRegistry.RegisterService(typeof(IXBeeService), xbeeService);
            ApplicationContext.ServiceRegistry.RegisterService(typeof(MqttService), mqttService);

            ApplicationContext.ServiceRegistry.RegisterService(typeof(Gateway), new Gateway(xbeeService, mqttService, messageBus));

            // Migration
            //ApplicationContext.ServiceRegistry.RegisterService(typeof(WatchdogService), new WatchdogService(messageBus, mqttService));

            Log.Debug("[MosziNet_HA] Starting up...");

            // setup the run loop participants
            mainRunLoop.AddRunLoopParticipant(messageBusRunner);
            mainRunLoop.AddRunLoopParticipant(mqttService);
            mainRunLoop.AddRunLoopParticipant(new XBeeServiceWrapper(xbeeService));
            mainRunLoop.AddRunLoopParticipant(new StatisticsService());

            mainRunLoop.Run();
        }

        /// <summary>
        /// Configuration necessary for the application is built in this method.
        /// </summary>
        private static void InitializeApplicationConfiguration()
        {
            ApplicationConfiguration configuration = new ApplicationConfiguration();
            ApplicationContext.Configuration = configuration;

            // set up the device types. Key is DD value from XBee frame, value is the class type that handles frames
            configuration.RegisterObjectForKey(ApplicationConfigurationCategory.DeviceTypeID, 0x9988, typeof(TemperatureDeviceV1));
            configuration.RegisterObjectForKey(ApplicationConfigurationCategory.DeviceTypeID, 0x9987, typeof(HeartBeatDevice));
            configuration.RegisterObjectForKey(ApplicationConfigurationCategory.DeviceTypeID, 0x9986, typeof(TemperatureDeviceV2));
            configuration.RegisterObjectForKey(ApplicationConfigurationCategory.DeviceTypeID, 0x9985, typeof(DoubleRelayLM35));
            configuration.RegisterObjectForKey(ApplicationConfigurationCategory.DeviceTypeID, 0x9984, typeof(DoubleRelay));

            // register xbee frame processors
            configuration.RegisterObjectForKey(ApplicationConfigurationCategory.XBeeFrameProcessor, typeof(RemoteCommandResponse), new RemoteCommandResponseProcessor());
            configuration.RegisterObjectForKey(ApplicationConfigurationCategory.XBeeFrameProcessor, typeof(IODataSample), new IODataSampleFrameProcessor());
        }

    }
}
