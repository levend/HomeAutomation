using System;

namespace MosziNet.HomeAutomation.XBee.Frame
{
    public interface IXBeeFrame
    {
        byte FrameType { get; }

        byte[] Address { get; set; }

        byte[] NetworkAddress { get; set; }
    }
}
