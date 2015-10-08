using System;

namespace MosziNet.HomeAutomation.XBee.Frame.ZigBee
{
    /// <summary>
    /// Send by the remote XBee in response to a <see cref="RemoteATCommand"/> frame.
    /// </summary>
    public class RemoteCommandResponse : BaseXBeeFrame
    {
        public byte FrameId { get; set; }

        public byte[] ATCommand { get; set; }

        public byte Status { get; set; }

        public byte[] Parameters { get; set; }
    }
}
