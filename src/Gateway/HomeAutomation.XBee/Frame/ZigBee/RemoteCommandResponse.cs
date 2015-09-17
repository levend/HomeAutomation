using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee.Frame.ZigBee
{
    public class RemoteCommandResponse : BaseXBeeFrame
    {
        public byte FrameId { get; set; }

        public byte[] ATCommand { get; set; }

        public byte Status { get; set; }

        public byte[] Parameters { get; set; }
    }
}
