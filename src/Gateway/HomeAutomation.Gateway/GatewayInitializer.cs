using MosziNet.HomeAutomation.Admin;
using MosziNet.HomeAutomation.ApplicationLogic.XBeeFrameProcessor;
using MosziNet.HomeAutomation.BusinessLogic;
using MosziNet.HomeAutomation.Configuration;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation.Device.Concrete;
using MosziNet.HomeAutomation.Gateway.Device.Concrete;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Logging.Formatter;
using MosziNet.HomeAutomation.Logging.Writer;
using MosziNet.HomeAutomation.Messaging;
using MosziNet.HomeAutomation.Mqtt;
using MosziNet.HomeAutomation.Service;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;

namespace MosziNet.HomeAutomation
{
    public class GatewayInitializer
    {
        // TODO refactor these configuration items to a separate project (or at least break it into 2 parts: platform and functionality config)
        public void Initialize(IXBeeSerialPort serialPort)
        {
            InitializeApplicationConfiguration();

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
            IXBeeService xbeeService = new XBeeService(serialPort);

            // Register all services to the service registry
            ApplicationContext.ServiceRegistry.RegisterService(typeof(DeviceRegistry), deviceRegistry);
            ApplicationContext.ServiceRegistry.RegisterService(typeof(IMessageBus), messageBus);

            ApplicationContext.ServiceRegistry.RegisterService(typeof(IXBeeService), xbeeService);
            ApplicationContext.ServiceRegistry.RegisterService(typeof(MqttService), mqttService);

            ApplicationContext.ServiceRegistry.RegisterService(typeof(MqttXBeeTranslator), new MqttXBeeTranslator(xbeeService, mqttService, messageBus));

            // Migration
            //ApplicationContext.ServiceRegistry.RegisterService(typeof(WatchdogService), new WatchdogService(messageBus, mqttService));

            Log.Debug("[MosziNet_HA] Starting up...");

            // setup the run loop participants
            mainRunLoop.AddRunLoopParticipant(messageBusRunner);
            mainRunLoop.AddRunLoopParticipant(mqttService);
            mainRunLoop.AddRunLoopParticipant(new XBeeServiceLoopableWrapper(xbeeService));
            mainRunLoop.AddRunLoopParticipant(new StatisticsService());

            mainRunLoop.Start();
        }

        /// <summary>
        /// Configuration necessary for the application is built in this method.
        /// </summary>
        private static void InitializeApplicationConfiguration()
        {
            ApplicationConfiguration configuration = new ApplicationConfiguration();
            ApplicationContext.Configuration = configuration;

            // set up the device types. Key is DD value from XBee frame, value is the class type that handles frames
            configuration.RegisterObjectForKey(ApplicationConfigurationCategory.DeviceTypeID, 0x9999, typeof(FakeTemperatureDevice));
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
