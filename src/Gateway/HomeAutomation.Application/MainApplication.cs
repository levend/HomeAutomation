using HomeAutomation.Application.Configuration;
using HomeAutomation.Gateway;

namespace HomeAutomation.Application
{
    public class MainApplication
    {
        public static void Start(string configurationFile)
        {
            var configuration = new ConfigurationManager().LoadFile<HomeAutomationConfiguration>(configurationFile);

            // the gateway must be initialized before registering any networks, controllers
            GatewayInitializer gateway = new GatewayInitializer(configuration.Gateway);

            ApplicationInitializer.Initialize(configuration);

            gateway.Run();
        }
    }
}
