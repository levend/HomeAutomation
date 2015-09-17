using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee
{
    /// <summary>
    /// Contains indexes that can be used in all XBee frame types.
    /// </summary>
    public static class FrameIndex
    {
        public static readonly byte Start = 0;
        public static readonly byte LengthMSB = 1;
        public static readonly byte LengthLSB = 2;
        public static readonly byte FrameType = 3;
    }
}