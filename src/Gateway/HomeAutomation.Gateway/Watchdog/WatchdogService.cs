using HomeAutomation.Core;
using HomeAutomation.Core.Service;
using System;
using Windows.System.Threading;

namespace HomeAutomation.Gateway.Watchdog
{
    public class WatchdogService : ICooperativeService
    {
        public WatchdogService(int periodInSeconds)
        {
            ThreadPoolTimer.CreatePeriodicTimer((source) =>
            {
                HomeAutomationSystem.ControllerRegistry.All.SendGatewayHeartbeatMessage($"Watcdog:{DateTime.Now}");
            }, TimeSpan.FromSeconds(periodInSeconds));
        }

        public void ExecuteTasks()
        {
            // todo: refactor class to use the cooperative service ...
        }
    }
}
