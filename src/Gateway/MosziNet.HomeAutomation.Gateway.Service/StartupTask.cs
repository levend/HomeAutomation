using MosziNet.HomeAutomation.NetCore.RPI;
using MosziNet.HomeAutomation.XBee;
using Windows.ApplicationModel.Background;
using Windows.Devices.SerialCommunication;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace MosziNet.HomeAutomation.Gateway.Service
{
    public sealed class StartupTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            //// get the serial port that's going to be used to acess the XBee
            IXBeeSerialPort serialPort = new XBeeSerialPort(9600, SerialParity.None, SerialStopBitCount.One, 8);

            //// start the gateway.
            //new GatewayInitializer().Initialize(serialPort);
        }
    }
}
