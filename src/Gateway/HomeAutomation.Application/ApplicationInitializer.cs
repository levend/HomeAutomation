using HomeAutomation.Application.Configuration;
using MosziNet.HomeAutomation.NetCore.RPI;
using MosziNet.HomeAutomation.XBee;
using Windows.Devices.SerialCommunication;

namespace HomeAutomation.Application
{
    public static class ApplicationInitializer
    {
        public static void Initialize(HomeAutomationConfiguration configuration)
        {
            InitializeDeviceNetworks(configuration);
        }

        private static void InitializeDeviceNetworks(HomeAutomationConfiguration configuration)
        {
            // get the serial port that's going to be used to acess the XBee network
            IXBeeSerialPort serialPort = new XBeeSerialPort(configuration.XBee.BaudRate, configuration.XBee.SerialParity, configuration.XBee.SerialStopBitCount, configuration.XBee.DataBits);
        }
    }
}
