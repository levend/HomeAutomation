﻿using HomeAutomation.Core;
using HomeAutomation.Core.Service;
using HomeAutomation.Gateway.Admin;
using HomeAutomation.Gateway.BusinessLogic;
using HomeAutomation.Gateway.Configuration;
using HomeAutomation.Gateway.Watchdog;
using HomeAutomation.Logging;

namespace HomeAutomation.Gateway
{
    public class GatewayInitializer
    {
        public GatewayInitializer(GatewayConfiguration config)
        {
            Log.Debug("[HomeAutomation.Gateway] Gateway initializing ...");

            HomeAutomationSystem.ServiceRegistry.RegisterService(new DeviceNetworkGateway());
            HomeAutomationSystem.ScheduledTasks.ScheduleTask(new WatchdogService(), config.WatchdogPeriodInSeconds);
            HomeAutomationSystem.ScheduledTasks.ScheduleTask(new StatisticsService(), config.StatisticsAnnouncementPeriodInSeconds);

            Log.Debug("[HomeAutomation.Gateway] Initialized.");
        }

        public void Run()
        {
            Log.Debug("[HomeAutomation.Gateway] Started.");

            HomeAutomationSystem.ScheduledTasks.Runner.Start();
        }
    }
}
