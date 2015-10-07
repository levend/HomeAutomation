using System;

namespace MosziNet.HomeAutomation.XBee.Frame
{
    /// <summary>
    /// Represents a frame received from XBee.
    /// </summary>
    public abstract class BaseXBeeFrame : IXBeeFrame
    {
        public Int32 Length { get; set; }

        public byte FrameType { get; set; }

        public byte[] Address { get; set; }

        public byte[] NetworkAddress { get; set; }
    }
}
