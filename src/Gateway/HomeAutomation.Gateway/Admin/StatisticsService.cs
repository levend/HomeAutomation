using HomeAutomation.Core;
using HomeAutomation.Core.Diagnostics;
using HomeAutomation.Core.Scheduler;
using MosziNet.XBee;
using System;

namespace HomeAutomation.Gateway.Admin
{
    public class StatisticsService : IScheduledTask
    {
        private Statistics systemStatistics;
        private DateTime lastMeasureTime;
        private int statisticsIntervalInSeconds;

        public StatisticsService()
        {
            this.statisticsIntervalInSeconds = 1;

            systemStatistics = new Statistics();
            systemStatistics.SystemStartTime = DateTime.Now;

            lastMeasureTime = DateTime.Now;
        }

        private void ReportStatistics()
        {
            HomeAutomationSystem.ControllerRegistry.All.SendStatistics(systemStatistics);
        }

        private void GatherStatistics()
        {
            systemStatistics.CurrentTime = DateTime.Now;
            systemStatistics.UptimeDays = systemStatistics.CurrentTime.Subtract(systemStatistics.SystemStartTime).Days;
        }

        public void TimeElapsed()
        {
            // todo: refactor to use a "scheduler"
            // check if it's time to gather statistics
            if (lastMeasureTime.AddSeconds(statisticsIntervalInSeconds) < DateTime.Now)
            {
                lastMeasureTime = DateTime.Now;

                GatherStatistics();
                ReportStatistics();

                // todo: refactor this for a smaller update period
                RequestDeviceNetworkDiagnostics();
            }
        }

        private void RequestDeviceNetworkDiagnostics()
        {
            IDeviceNetwork[] networkList = HomeAutomationSystem.DeviceNetworkRegistry.GetDeviceNetworks();
            foreach(IDeviceNetwork oneNetwork in networkList)
            {
                oneNetwork.UpdateDiagnostics();
            }
        }
    }
}
