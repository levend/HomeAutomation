using HomeAutomation.Application;
using HomeAutomation.Application.Configuration;
using Windows.ApplicationModel.Background;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace HomeAutomation.Gateway.Service
{
    public sealed class StartupTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var configuration = new ConfigurationManager().LoadFile("Config/HomeAutomation.conf");

            GatewayInitializer gateway = new GatewayInitializer();

            ApplicationInitializer.Initialize(configuration);

            gateway.Initialize(configuration.Gateway);
            gateway.Run();
        }
    }
}
