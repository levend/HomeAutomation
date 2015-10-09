using HomeAutomation.Core;
using System;
using Windows.System.Threading;

namespace MosziNet.HomeAutomation.Gateway.Watchdog
{
    public class WatchdogService
    {
        public WatchdogService(int periodInSeconds)
        {
            ThreadPoolTimer.CreatePeriodicTimer((source) =>
            {
                HomeAutomationSystem.ControllerRegistry.All.SendGatewayHeartbeatMessage($"Watcdog:{DateTime.Now}");
            }, TimeSpan.FromSeconds(periodInSeconds));
        }
    }
}
