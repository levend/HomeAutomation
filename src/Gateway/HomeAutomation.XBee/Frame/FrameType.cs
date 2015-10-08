using System;

namespace MosziNet.HomeAutomation.XBee.Frame
{
    /// <summary>
    /// Contains the frame types known by the system.
    /// </summary>
    public enum FrameType : Byte
    {
        IODataSample = 0x92,
        RemoteATCommand = 0x17,
        RemoteCommandResponse = 0x97
    }
}
