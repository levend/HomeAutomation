using MosziNet.HomeAutomation.Gateway.Admin;
using MosziNet.HomeAutomation.Gateway.BusinessLogic;
using MosziNet.HomeAutomation.Gateway.Messaging;
using MosziNet.HomeAutomation.Gateway.Service;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Logging.Formatter;
using MosziNet.HomeAutomation.Logging.Writer;

namespace MosziNet.HomeAutomation
{
    public class GatewayInitializer
    {
        // TODO refactor these configuration items to a separate project (or at least break it into 2 parts: platform and functionality config)
        public void Initialize()
        {
            ApplicationContext.ServiceRegistry = new ServiceRegistry();

            RunLoop mainRunLoop = new RunLoop();

            //// Set up the mqtt service
            //MqttService mqttService = new MqttService(new MosziNet.HomeAutomation.Gateway.Configuration.MqttServerConfiguration(
            //    "192.168.1.213",
            //    20,
            //    "MosziNet_HomeAutomation_Gateway_v1.1",
            //    "/MosziNet_HA"));

            // Setup the logging framework
            //Log.AddLogWriter(new MqttLogWriter(mqttService, MqttTopic.LogTopic), new StandardLogFormatter());
            Log.AddLogWriter(new ConsoleLogWriter(), new StandardLogFormatter());

            // Setup the message bus
            IMessageBus messageBus = new MessageBus();
            ThreadedMessageBusRunner messageBusRunner = new ThreadedMessageBusRunner(messageBus, new MessageProcessorRegistry());
            messageBus.MessageBusRunner = messageBusRunner;            

            // Register all services to the service registry
            ApplicationContext.ServiceRegistry.RegisterService(typeof(IMessageBus), messageBus);
            //ApplicationContext.ServiceRegistry.RegisterService(typeof(MqttService), mqttService);

            //ApplicationContext.ServiceRegistry.RegisterService(typeof(DeviceNetworkGateway), new DeviceNetworkGateway(mqttService, messageBus));

            // Migration
            //ApplicationContext.ServiceRegistry.RegisterService(typeof(WatchdogService), new WatchdogService(messageBus, mqttService));

            Log.Debug("[MosziNet_HA] Starting up...");

            // setup the run loop participants
            mainRunLoop.AddRunLoopParticipant(messageBusRunner);
            //mainRunLoop.AddRunLoopParticipant(mqttService);
            //mainRunLoop.AddRunLoopParticipant(new XBeeServiceLoopableWrapper(xbeeService));
            mainRunLoop.AddRunLoopParticipant(new StatisticsService());

            mainRunLoop.Start();
        }
    }
}
