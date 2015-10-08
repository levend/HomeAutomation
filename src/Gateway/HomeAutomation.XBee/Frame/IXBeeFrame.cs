using System;

namespace MosziNet.HomeAutomation.XBee.Frame
{
    public interface IXBeeFrame
    {
        FrameType FrameType { get; }

        byte[] Address { get; set; }

        byte[] NetworkAddress { get; set; }
    }
}
