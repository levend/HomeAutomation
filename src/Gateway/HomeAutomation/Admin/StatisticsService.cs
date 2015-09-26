using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.Logging;
using System.Text;

namespace MosziNet.HomeAutomation.Admin
{
    public class StatisticsService : IRunLoopParticipant
    {
        private DateTime lastMeasureTime;
        private int StatisticsIntervalInSeconds = 10;

        public StatisticsService()
        {
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

            Log.Debug(new StringBuilder()
                .Append("System uptime in days: " + Statistics.UptimeDays + "\n")
                .Append("XBee messages received: " + Statistics.XBeeMessageReceiveCount + "\n")
                .Append("XBee messages sent: " + Statistics.XBeeMessageSentCount + "\n")
                .Append("XBee messages dropped: " + Statistics.XBeeMessageDropCount + "\n")
                .Append("Free memory: " + Statistics.FreeMemory)
                .ToString());
        }

        private void GatherStatistics()
        {
            TimeSpan uptime = DateTime.Now.Subtract(Statistics.SystemStartTime);
            Statistics.UptimeDays = uptime.Days;

            Statistics.XBeeMessageDropCount = XBeeStatistics.MessagesDiscarded;
            Statistics.XBeeMessageReceiveCount = XBeeStatistics.MessagesReceived;
            Statistics.XBeeMessageSentCount = XBeeStatistics.MessagesSent;

            Statistics.FreeMemory = Microsoft.SPOT.Debug.GC(false);
        }
    }
}
