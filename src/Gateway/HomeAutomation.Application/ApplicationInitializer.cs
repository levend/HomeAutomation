using HomeAutomation.Application.Configuration;
using HomeAutomation.Communication.Mqtt;
using HomeAutomation.Core;
using HomeAutomation.Core.Controller;
using HomeAutomation.Core.Network;
using HomeAutomation.Core.Scheduler;
using HomeAutomation.Logging;
using HomeAutomation.Logging.Formatter;
using HomeAutomation.Logging.Writer;
using System;

namespace HomeAutomation.Application
{
    public class ApplicationInitializer
    {
        private HomeAutomationConfiguration configuration;

        public void Initialize(HomeAutomationConfiguration configuration)
        {
            this.configuration = configuration;

            InitializeDeviceNetworks();

            InitializeDeviceTypes();

            InitializeControllers();

            InitializeLogging();
        }

        private void InitializeLogging()
        {
            Log.AddLogWriter(new ConsoleLogWriter(), new StandardLogFormatter());

            if (!String.IsNullOrEmpty(configuration.Logging.MqttServerName))
            {
                MqttServerConfiguration mqttConfig = new MqttServerConfiguration()
                {
                    ClientName = "HomeAutomation.Gateway.MqttLogWriter",
                    KeepAliveCheckPeriodInSeconds = 20,
                    ServerHostName = configuration.Logging.MqttServerName,
                    TopicRootName = configuration.Logging.RootTopicName
                };

                MqttService mqttService = new MqttService(mqttConfig, new MqttClientWrapper(configuration.Logging.MqttServerName));
                HomeAutomationSystem.ScheduledTasks.ScheduleRealtimeTask(mqttService);

                Log.AddLogWriter(new MqttLogWriter(mqttService, configuration.Logging.SubTopicName), new StandardLogFormatter());
            }
        }

        private void InitializeDeviceNetworks()
        {
            foreach(DeviceNetworkConfiguration dnc in configuration.DeviceNetworks)
            {
                IDeviceNetworkFactory factory = Activator.CreateInstance(Type.GetType(dnc.Factory)) as IDeviceNetworkFactory;
                IDeviceNetwork network = factory.CreateDeviceNetwork(dnc.Configuration);

                HomeAutomationSystem.DeviceNetworkRegistry.RegisterDeviceNetwork(network, dnc.Name);

                HomeAutomationSystem.ScheduledTasks.ScheduleRealtimeTask(network as IScheduledTask);
            }
        }

        private void InitializeDeviceTypes()
        {
            foreach(DeviceTypeDescription dtd in configuration.DeviceTypes)
            {
                HomeAutomationSystem.DeviceTypeRegistry.RegisterDeviceType(dtd);
            }
        }

        private void InitializeControllers()
        {
            foreach(ControllerConfiguration cc in configuration.Controllers)
            {
                IControllerFactory factory = Activator.CreateInstance(Type.GetType(cc.Factory)) as IControllerFactory;

                IController controller = factory.CreateController(cc.Configuration);

                HomeAutomationSystem.ControllerRegistry.RegisterController(controller);

                HomeAutomationSystem.ScheduledTasks.ScheduleRealtimeTask(controller as IScheduledTask);
            }
        }
    }
}
