using System;
using HomeAutomation.Core.Scheduler;
using HomeAutomation.Core;

namespace HomeAutomation.Gateway.Watchdog
{
    public class WatchdogService : IScheduledTask
    {
        // todo: refactor class to use the cooperative service ...
        public WatchdogService()
        {
            //ThreadPoolTimer.CreatePeriodicTimer((source) =>
            //{
            //    HomeAutomationSystem.ControllerRegistry.All.SendGatewayHeartbeatMessage($"Watcdog:{DateTime.Now}");
            //}, TimeSpan.FromSeconds(periodInSeconds));
        }

        public void TimeElapsed()
        {
            
        }
    }
}
