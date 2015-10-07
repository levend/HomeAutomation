using System;

namespace MosziNet.HomeAutomation.XBee
{
    public static class XBeeStatistics
    {
        public static UInt64 MessagesReceived { get; set; }
        public static UInt64 MessagesSent { get; set; }
        public static UInt64 MessagesDiscarded { get; set; }
    }
}
