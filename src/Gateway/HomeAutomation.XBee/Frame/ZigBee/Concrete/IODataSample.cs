using System;

namespace MosziNet.HomeAutomation.XBee.Frame.ZigBee
{
    /// <summary>
    /// Contains IO pin samples from an XBee.
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
