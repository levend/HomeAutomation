using HomeAutomation.Core;
using HomeAutomation.Gateway.Admin;
using HomeAutomation.Gateway.BusinessLogic;
using HomeAutomation.Gateway.Configuration;
using HomeAutomation.Gateway.Service;
using HomeAutomation.Gateway.Watchdog;
using HomeAutomation.Logging;

namespace HomeAutomation.Gateway
{
    public class GatewayInitializer
    {
        private ServiceRunner serviceRunner = new ServiceRunner();

        public void Initialize(GatewayConfiguration config)
        {
            Log.Debug("[HomeAutomation.Gateway] Gateway initializing ...");

            ServiceRegistry.Instance.RegisterService(typeof(DeviceNetworkGateway), new DeviceNetworkGateway());
            ServiceRegistry.Instance.RegisterService(typeof(WatchdogService), new WatchdogService(config.WatchdogPeriodInSeconds));
            ServiceRegistry.Instance.RegisterService(typeof(StatisticsService), new StatisticsService(config.StatisticsAnnouncementPeriodInSeconds));

            AddDeviceNetworks();
            AddControllers();

            Log.Debug("[HomeAutomation.Gateway] Initialized.");
        }

        private void AddControllers()
        {
            ICooperativeService[] list = HomeAutomationSystem.ControllerRegistry.GetControllers();
            foreach (ICooperativeService item in list)
            {
                ServiceRegistry.Instance.RegisterService(item);
            }
        }

        private void AddDeviceNetworks()
        {
            ICooperativeService[] list = HomeAutomationSystem.DeviceNetworkRegistry.GetDeviceNetworks();
            foreach(ICooperativeService item in list)
            {
                ServiceRegistry.Instance.RegisterService(item);
            }
        }

        public void Run()
        {
            Log.Debug("[HomeAutomation.Gateway] Started.");

            serviceRunner.Start();
        }
    }
}
