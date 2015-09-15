using System;
using Microsoft.SPOT;
using System.IO.Ports;

namespace MosziNet.HomeAutomation.XBee.Frame
{
    /// <summary>
    /// Represents a frame received from XBee.
    /// </summary>
    public abstract class BaseXBeeFrame : IXBeeFrame
    {
        private FrameType frameType;

        public FrameType FrameType
        {
            get
            {
                return frameType;
            }

            internal set
            {
                frameType = value;
            }
        }

        public byte[] Address { get; set; }
    }
}
