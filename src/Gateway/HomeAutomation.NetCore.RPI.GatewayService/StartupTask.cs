using MosziNet.HomeAutomation.XBee;
using Windows.ApplicationModel.Background;

namespace MosziNet.HomeAutomation.NetCore.RPI.GatewayService
{
    public sealed class StartupTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // get the serial port that's going to be used to acess the XBee
            ISerialPort serialPort = new XBeeSerialPort();

            // start the gateway.
            new Gateway().Initialize(serialPort);
        }
    }
}
