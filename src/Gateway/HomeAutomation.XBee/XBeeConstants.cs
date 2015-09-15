using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee
{
    /// <summary>
    /// Contains various constants used in decoding the XBee frames.
    /// </summary>
    public static class XBeeConstants
    {
        public static readonly byte FrameStart = 0x7e;

        // we limit the max frame length to 200 bytes - anything above this size will be discarded.
        public static readonly byte MaxFrameLength = 200;
    }
}
