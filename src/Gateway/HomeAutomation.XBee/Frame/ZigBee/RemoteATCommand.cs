using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee.Frame.ZigBee
{
    /// <summary>
    /// Example frame for remote DD command: 7E 00 0F 17 01 00 13 A2 00 40 D6 AB 41 FF FE 00 44 44 AB
    /// </summary>
    public class RemoteATCommand : BaseXBeeFrame
    {
        public const byte OptionCommitChanges = 2;

        public RemoteATCommand()
        {
            this.FrameType = Frame.FrameType.RemoteATCommand;
        }

        public byte FrameId { get; set; }

        public byte CommandOptions { get; set; }

        public byte[] ATCommand { get; set; }

        public byte[] Parameters { get; set; }
    }
}
