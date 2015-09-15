using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee.Frame
{
    /// <summary>
    /// Contains readings from an XBee. Eg frame: 7E 00 12 92 00 13 A2 00 40 E4 38 E3 82 8F 01 01 00 00 01 02 6A F9
    /// </summary>
    public class IODataSampleFrame : BaseXBeeFrame
    {
        /// <summary>
        /// Data samples read by the xbee.
        /// </summary>
        public double[] AnalogReadings { get; set; }
    }
}
