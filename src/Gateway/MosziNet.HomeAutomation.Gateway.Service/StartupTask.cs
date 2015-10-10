using HomeAutomation.Application;
using HomeAutomation.Application.Configuration;
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
            ApplicationInitializer.Initialize(new ConfigurationManager().LoadFile("Config/HomeAutomation.conf"));
        }
    }
}
