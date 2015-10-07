using MosziNet.HomeAutomation.XBee;
using Windows.ApplicationModel.Background;
using Windows.Devices.SerialCommunication;

namespace MosziNet.HomeAutomation.NetCore.RPI.GatewayService
{
    public sealed class StartupTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // get the serial port that's going to be used to acess the XBee
            IXBeeSerialPort serialPort = new XBeeSerialPort(9600, SerialParity.None, SerialStopBitCount.One, 8);

            // start the gateway.
            new Gateway().Initialize(serialPort);
        }
    }
}
