using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee.Frame.ZigBee
{
    public class ATCommands
    {
        public static readonly byte[] DD = { 0x44, 0x44 };

        public static readonly byte[] D1 = { 0x44, 0x31 };

        public static readonly byte[] D2 = { 0x44, 0x32 };

        public static readonly byte[] AC = { 0x41, 0x43 };
    }
}
