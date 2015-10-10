using HomeAutomation.Core;
using HomeAutomation.Logging;
using MosziNet.HomeAutomation.XBee;
using System;
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

            String statisticsMessage = new StringBuilder()
                .Append("System uptime in days: " + systemStatistics.UptimeDays + "\n")
                .Append("XBee messages received: " + systemStatistics.XBeeMessageReceiveCount + "\n")
                .Append("XBee messages sent: " + systemStatistics.XBeeMessageSentCount + "\n")
                .Append("Free memory: " + systemStatistics.FreeMemory)
                .ToString();

            Log.Debug(statisticsMessage);
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