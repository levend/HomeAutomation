using System;
using Microsoft.SPOT;
using System.Collections;

namespace MosziNet.HomeAutomation.XBee.Frame.Serialization
{

    /// <summary>
    /// Example frame for remote DD command: 7E 00 0F 17 01 00 13 A2 00 40 D6 AB 41 FF FE 00 44 44 AB
    /// </summary>
    public class RemoteATCommandSerializer : BaseFrameSerializer
    {
        public override void Deserialize(IXBeeFrame frame, byte[] buffer, int length)
        {
            
        }

        public override int SerializeFrameContent(IXBeeFrame frame, byte[] resultArray, int offset)
        {
            RemoteATCommandFrame atFrame = (RemoteATCommandFrame)frame;

            // add the remote command options
            resultArray[offset++] = 0x00;

            // add the remote command itself
            resultArray[offset++] = atFrame.ATCommand[0];
            resultArray[offset++] = atFrame.ATCommand[1];

            return offset;
        }

        public override FrameType FrameType
        {
            get { return Frame.FrameType.RemoteATCommand; }
        }
    }
}
