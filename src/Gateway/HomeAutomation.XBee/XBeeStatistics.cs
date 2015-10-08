using System;

namespace MosziNet.HomeAutomation.XBee
{
    /// <summary>
    /// Keeps statistics on the XBee events.
    /// </summary>
    public static class XBeeStatistics
    {
        /// <summary>
        /// The number of messages received on the XBee.
        /// </summary>
        public static UInt64 MessagesReceived { get; set; }

        /// <summary>
        /// The number of messages sent by the XBee.
        /// </summary>
        public static UInt64 MessagesSent { get; set; }
    }
}
