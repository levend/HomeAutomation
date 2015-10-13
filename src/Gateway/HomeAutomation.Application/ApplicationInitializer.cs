using HomeAutomation.Application.Configuration;
using HomeAutomation.Communication.Mqtt;
using HomeAutomation.Controller.Mqtt;
using HomeAutomation.Core;
using HomeAutomation.DeviceNetwork.XBee;
using HomeAutomation.Logging;
using HomeAutomation.Logging.Formatter;
using HomeAutomation.Logging.Writer;
using MosziNet.HomeAutomation.XBee;

namespace HomeAutomation.Application
{
    public class ApplicationInitializer
    {
        MqttService mqttService;

        public void Initialize(HomeAutomationConfiguration configuration)
        {
            InitializeDeviceNetworks(configuration);

            InitializeControllers(configuration);

            InitializeLogging();
        }

        private void InitializeLogging()
        {
            Log.AddLogWriter(new ConsoleLogWriter(), new StandardLogFormatter());
            Log.AddLogWriter(new MqttLogWriter(mqttService, MqttTopic.LogTopic), new StandardLogFormatter());
        }

        private void InitializeControllers(HomeAutomationConfiguration configuration)
        {
            IMqttClient mqttClient = configuration.MqttClientFactory.Create(configuration.Mqtt);
            mqttService = new MqttService(configuration.Mqtt, mqttClient);

            HomeAutomationSystem.ScheduledTasks.ScheduleTask(mqttService, 15); // TODO: move this value to the configuration file

            MqttController mqttController = new MqttController(mqttService);

            HomeAutomationSystem.ControllerRegistry.RegisterController(mqttController);
        }

        private void InitializeDeviceNetworks(HomeAutomationConfiguration configuration)
        {
            // get the serial port that's going to be used to acess the XBee network
            IXBeeSerialPort serialPort = configuration.XBeeSerialPortFactory.Create(configuration.XBee);
            
            XBeeDeviceNetwork xbeeNetwork = new XBeeDeviceNetwork(serialPort);
            
            HomeAutomationSystem.DeviceNetworkRegistry.RegisterDeviceNetwork(xbeeNetwork, "xbee");

            HomeAutomationSystem.ScheduledTasks.ScheduleRealtimeTask(xbeeNetwork); 
        }
    }
}
