using HomeAutomation.Application.Configuration;
using HomeAutomation.Gateway;

namespace HomeAutomation.Application
{
    public class MainApplication
    {
        public static void Start(string configurationFile)
        {
            var configuration = new ConfigurationManager().LoadFile<HomeAutomationConfiguration>(configurationFile);

            GatewayInitializer gateway = new GatewayInitializer();

            ApplicationInitializer.Initialize(configuration);

            gateway.Initialize(configuration.Gateway);
            gateway.Run();
        }
    }
}
