using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee
{
    public static class FrameIndex
    {
        public static readonly byte Start = 0;
        public static readonly byte LengthMSB = 1;
        public static readonly byte LengthLSB = 2;
        public static readonly byte FrameType = 3;
        public static readonly byte Address = 4;
    }
}
