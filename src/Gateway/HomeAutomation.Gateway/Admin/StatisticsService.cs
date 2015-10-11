using HomeAutomation.Core;
using HomeAutomation.Core.Service;
using HomeAutomation.Logging;
using MosziNet.HomeAutomation.XBee;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAutomation.Gateway.Admin
{
    public class StatisticsService : ICooperativeService
    {
        private Statistics systemStatistics;
        private DateTime lastMeasureTime;
        private int statisticsIntervalInSeconds;

        public StatisticsService(int statisticsIntervalInSeconds)
        {
            this.statisticsIntervalInSeconds = statisticsIntervalInSeconds;

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

        public void ExecuteTasks()
        {
            // check if it's time to gather statistics
            if (lastMeasureTime.AddSeconds(statisticsIntervalInSeconds) < DateTime.Now)
            {
                GatherStatistics();

                ReportStatistics();
            }
        }
    }
}
