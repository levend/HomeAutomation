using System;

namespace HomeAutomation.Gateway.Admin
{
    public class Statistics
    {
        // System
        public DateTime SystemStartTime { get; set; }

        public Int32 UptimeDays { get; set; }

        public UInt32 FreeMemory { get; set; }

        public UInt64 XBeeMessageSentCount { get; set; }

        public UInt64 XBeeMessageReceiveCount { get; set; }

        ////
        //public double XBeeMessageSentPerMinuteCount { get; set; }

        //// Per Minute Statistics
        //public double XBeeMessageReceivedPerMinuteCount { get; set; }

        //public double XBeeMessageSentPerMinuteCount { get; set; }

        //public double XBeeMessageReceivedPerMinuteCount { get; set; }
    }
}