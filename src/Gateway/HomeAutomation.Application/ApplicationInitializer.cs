using HomeAutomation.Application.Configuration;
using HomeAutomation.Application.Factory;
using HomeAutomation.Communication.Mqtt;
using HomeAutomation.Controller.Mqtt;
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
        public void Initialize(HomeAutomationConfiguration configuration)
        {
            InitializeDeviceNetworks(configuration);

            InitializeDeviceTypes(configuration);

            InitializeControllers(configuration);

            InitializeLogging();
        }

        private void InitializeLogging()
        {
            Log.AddLogWriter(new ConsoleLogWriter(), new StandardLogFormatter());
            //Log.AddLogWriter(new MqttLogWriter(mqttService, MqttTopic.LogTopic), new StandardLogFormatter());
        }

        private void InitializeDeviceNetworks(HomeAutomationConfiguration configuration)
        {
            foreach(DeviceNetworkConfiguration dnc in configuration.DeviceNetworks)
            {
                IDeviceNetworkFactory factory = Activator.CreateInstance(Type.GetType(dnc.Factory)) as IDeviceNetworkFactory;
                IDeviceNetwork network = factory.CreateDeviceNetwork(dnc.Configuration);

                HomeAutomationSystem.DeviceNetworkRegistry.RegisterDeviceNetwork(network, dnc.Name);

                HomeAutomationSystem.ScheduledTasks.ScheduleRealtimeTask(network as IScheduledTask);
            }
        }

        private void InitializeDeviceTypes(HomeAutomationConfiguration configuration)
        {
            foreach(DeviceTypeDescription dtd in configuration.DeviceTypes)
            {
                HomeAutomationSystem.DeviceTypeRegistry.RegisterDeviceType(dtd);
            }
        }

        // todo: refactor to get this info from the config file.
        private void InitializeControllers(HomeAutomationConfiguration configuration)
        {
            foreach(ControllerConfiguration cc in configuration.Controllers)
            {
                IControllerFactory factory = Activator.CreateInstance(Type.GetType(cc.Factory)) as IControllerFactory;

                IHomeController controller = factory.CreateController(cc.Configuration);

                HomeAutomationSystem.ControllerRegistry.RegisterController(controller);

                HomeAutomationSystem.ScheduledTasks.ScheduleRealtimeTask(controller as IScheduledTask);
            }
        }
    }
}
