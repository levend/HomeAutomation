using HomeAutomation.Application.Configuration;
using HomeAutomation.Core;
using HomeAutomation.Gateway;
using HomeAutomation.Logging;

namespace HomeAutomation.Application
{
    public class MainApplication
    {
        static GatewayInitializer gateway;

        public static void Initialize(string configurationFile)
        {
            var configuration = new ConfigurationManager().LoadFile<HomeAutomationConfiguration>(configurationFile);

            // the gateway must be initialized before registering any networks, controllers
            gateway = new GatewayInitializer(configuration.Gateway);

            new ApplicationInitializer().Initialize(configuration);
        }

        public static void Run()
        {
            HomeAutomationSystem.ScheduledTasks.Runner.Start();

            Log.Debug("[HomeAutomation] Started.");
        }
    }
}
