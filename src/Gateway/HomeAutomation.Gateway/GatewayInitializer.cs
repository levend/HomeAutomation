using HomeAutomation.Core;
using HomeAutomation.Core.Diagnostics;
using HomeAutomation.Gateway.Configuration;
using HomeAutomation.Logging;

namespace HomeAutomation.Gateway
{
    public class GatewayInitializer
    {
        public GatewayInitializer(GatewayConfiguration config)
        {
            Log.Debug("[HomeAutomation.Gateway] Gateway initializing ...");

            HomeAutomationSystem.ServiceRegistry.RegisterService(new Core.Gateway());
            HomeAutomationSystem.ScheduledTasks.ScheduleTask(new StatisticsService(), config.StatisticsAnnouncementPeriodInSeconds);

            Log.Debug("[HomeAutomation.Gateway] Initialized.");
        }
    }
}
