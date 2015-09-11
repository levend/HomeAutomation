using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee.Frame
{
    public interface IXBeeFrame
    {
        FrameType FrameType { get; }

        byte[] Address { get; set; }
    }
}
