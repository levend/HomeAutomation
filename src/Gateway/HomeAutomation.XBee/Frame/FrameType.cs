using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee.Frame
{
    public enum FrameType : byte
    {
        IODataSample = 0x92,
        RemoteATCommand = 0x17,
        RemoteCommandResponse = 0x97
    }
}
