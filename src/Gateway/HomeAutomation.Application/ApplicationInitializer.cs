using HomeAutomation.Application.Configuration;
using HomeAutomation.Communication.Mqtt;
using HomeAutomation.Controller.Mqtt;
using HomeAutomation.Core;
using HomeAutomation.DeviceNetwork.XBee;
using MosziNet.HomeAutomation.Gateway;
using MosziNet.HomeAutomation.Gateway.Service;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Logging.Formatter;
using MosziNet.HomeAutomation.Logging.Writer;
using MosziNet.HomeAutomation.NetCore.RPI;
using MosziNet.HomeAutomation.XBee;

namespace HomeAutomation.Application
{
    public static class ApplicationInitializer
    {
        public static void Initialize(HomeAutomationConfiguration configuration)
        {
            InitializeLogging();

            InitializeDeviceNetworks(configuration);

            InitializeControllers(configuration);

            new Gateway().Initialize(configuration.Gateway);
        }

        private static void InitializeLogging()
        {
            Log.AddLogWriter(new ConsoleLogWriter(), new StandardLogFormatter());
        }

        private static void InitializeControllers(HomeAutomationConfiguration configuration)
        {
            MqttService mqttService = new MqttService(configuration.Mqtt);
            ServiceRegistry.Instance.RegisterService(mqttService);

            MqttController mqttController = new MqttController(mqttService);

            HomeAutomationSystem.ControllerRegistry.RegisterController(mqttController);
        }

        private static void InitializeDeviceNetworks(HomeAutomationConfiguration configuration)
        {
            // get the serial port that's going to be used to acess the XBee network
            IXBeeSerialPort serialPort = new XBeeSerialPort(configuration.XBee.BaudRate, configuration.XBee.SerialParity, configuration.XBee.SerialStopBitCount, configuration.XBee.DataBits);
            
            XBeeDeviceNetwork xbeeNetwork = new XBeeDeviceNetwork(serialPort);
            
            HomeAutomationSystem.DeviceNetworkRegistry.RegisterDeviceNetwork(xbeeNetwork, "xbee");
        }
    }
}
