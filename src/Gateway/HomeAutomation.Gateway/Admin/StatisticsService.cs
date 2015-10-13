using HomeAutomation.Core;
using HomeAutomation.Core.Scheduler;
using MosziNet.HomeAutomation.XBee;
using System;
using System.Collections.Generic;

namespace HomeAutomation.Gateway.Admin
{
    public class StatisticsService : IScheduledTask
    {
        private Statistics systemStatistics;
        private DateTime lastMeasureTime;
        private int statisticsIntervalInSeconds;

        public StatisticsService()
        {
            this.statisticsIntervalInSeconds = 10;

            systemStatistics = new Statistics();
            systemStatistics.SystemStartTime = DateTime.Now;

            lastMeasureTime = DateTime.Now;
        }

        private void ReportStatistics()
        {
            lastMeasureTime = DateTime.Now;
            Dictionary<string, object> statistics = new Dictionary<string, object>();

            statistics.Add("XBeeMessageReceiveCount", systemStatistics.XBeeMessageReceiveCount);

            HomeAutomationSystem.ControllerRegistry.All.SendStatistics(statistics);
        }

        private void GatherStatistics()
        {
            TimeSpan uptime = DateTime.Now.Subtract(systemStatistics.SystemStartTime);
            systemStatistics.UptimeDays = uptime.Days;

            systemStatistics.XBeeMessageReceiveCount = XBeeStatistics.MessagesReceived;
            systemStatistics.XBeeMessageSentCount = XBeeStatistics.MessagesSent;

            // Migration
            //systemStatistics.FreeMemory = 0;
        }

        public void TimeElapsed()
        {
            // todo: refactor to use a "scheduler"
            // check if it's time to gather statistics
            if (lastMeasureTime.AddSeconds(statisticsIntervalInSeconds) < DateTime.Now)
            {
                GatherStatistics();

                ReportStatistics();
            }
        }
    }
}
