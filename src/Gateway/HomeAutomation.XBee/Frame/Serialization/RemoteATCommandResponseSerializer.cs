using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.XBee.Frame.Serialization
{
    public class RemoteATCommandResponseSerializer : BaseFrameSerializer
    {
        public override FrameType FrameType
        {
            get { return Frame.FrameType.RemoteCommandResponse; }
        }

        public override int SerializeFrameContent(IXBeeFrame frame, byte[] resultArray, int offset)
        {
            return 0;
        }

        public override void Deserialize(IXBeeFrame frame, byte[] buffer, int length)
        {
            base.Deserialize(frame, buffer, length);

            Debug.Print("Remote response: " + HexConverter.ToSpacedHexString(buffer, 0, length));
        }
    }
}
