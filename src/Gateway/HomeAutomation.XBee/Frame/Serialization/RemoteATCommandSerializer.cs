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
        public void Deserialize(IXBeeFrame frame, byte[] buffer)
        {
            
        }

        public int Serialize(RemoteATCommandFrame frame, byte[] resultArray, int offset)
        {
            int index = base.Serialize(frame, resultArray, offset);

            // add the remote command options
            resultArray[index++] = 0x00;

            // add the remote command itself
            resultArray[index++] = frame.ATCommand[0];
            resultArray[index++] = frame.ATCommand[1];

            resultArray[index++] = CalculateChecksum(resultArray, index);

            return index;
        }

        public override FrameType FrameType
        {
            get { return Frame.FrameType.RemoteATCommand; }
        }
    }
}
