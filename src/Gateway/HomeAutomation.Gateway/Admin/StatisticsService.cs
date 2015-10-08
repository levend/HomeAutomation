using System;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.Logging;
using System.Text;

namespace MosziNet.HomeAutomation.Admin
{
    public class StatisticsService : IRunLoopParticipant
    {
        private Statistics systemStatistics;
        private DateTime lastMeasureTime;
        private int StatisticsIntervalInSeconds = 5 * 60;

        public StatisticsService()
        {
            systemStatistics = new Statistics();
            systemStatistics.SystemStartTime = DateTime.Now;

            lastMeasureTime = DateTime.Now;
        }

        public void Execute()
        {
            // check if it's time to gather statistics
            if (lastMeasureTime.AddSeconds(StatisticsIntervalInSeconds) < DateTime.Now)
            {
                GatherStatistics();

                ReportStatistics();
            }
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
    }
}
