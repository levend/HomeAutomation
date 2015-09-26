using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device;

namespace MosziNet.HomeAutomation.Admin
{
    public static class Statistics
    {
        // System
        public static DateTime SystemStartTime { get; set; }

        public static Int32 UptimeDays { get; set; }

        public static UInt32 FreeMemory { get; set; }

        // Totals
        public static UInt64 XBeeMessageReceiveCount { get; set; }

        public static UInt64 XBeeMessageSentCount { get; set; }

        public static UInt64 XBeeMessageDropCount { get; set; }

        ////
        //public static double XBeeMessageSentPerMinuteCount { get; set; }

        //// Per Minute Statistics
        //public static double XBeeMessageReceivedPerMinuteCount { get; set; }

        //public static double XBeeMessageSentPerMinuteCount { get; set; }

        //public static double XBeeMessageReceivedPerMinuteCount { get; set; }
    }
}
