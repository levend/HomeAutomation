using System;

namespace MosziNet.HomeAutomation.XBee.Frame.ZigBee
{
    /// <summary>
    /// Contains readings from an XBee. Eg frame: 7E 00 12 92 00 13 A2 00 40 E4 38 E3 82 8F 01 01 00 00 01 02 6A F9
    /// </summary>
    public class IODataSample : BaseXBeeFrame
    {
        public byte ReceiveOptions { get; set; }

        public byte SampleCount { get; set; }

        public int DigitalMask { get; set; }

        public byte AnalogMask { get; set; }

        public byte[] Samples { get; set; }
    }
}
