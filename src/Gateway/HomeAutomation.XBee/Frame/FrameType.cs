using System;
using Microsoft.SPOT;
using System.Collections;

namespace MosziNet.HomeAutomation.XBee.Frame
{
    public class FrameType
    {
        private static Hashtable frameTypes = new Hashtable();

        public static readonly byte IODataSample = 0x92;
        public static readonly byte RemoteATCommand = 0x17;
        public static readonly byte RemoteCommandResponse = 0x97;

        static FrameType()
        {
            frameTypes.Add(IODataSample, "IODataSample");
            frameTypes.Add(RemoteATCommand, "RemoteATCommand");
            frameTypes.Add(RemoteCommandResponse, "RemoteCommandResponse");
        }

        public static string GetTypeName(byte frameType)
        {
            return frameTypes.Contains(frameType) ? (string)frameTypes[frameType] : null;
        }
    }
}
