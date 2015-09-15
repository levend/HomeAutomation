using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame;

namespace MosziNet.HomeAutomation.XBee
{
    public interface IXbeeFrameSerializer
    {
        FrameType FrameType { get; }

        /// <summary>
        /// Deserializes the buffer into the frame.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="frame"></param>
        /// <param name="length"></param>
        void Deserialize(IXBeeFrame frame, byte[] buffer, int length);

        /// <summary>
        /// Serializes the frame into the resultArray buffer starting with the offset position. Return the index of the next available position where writing can continue.
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="resultArray"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        int Serialize(IXBeeFrame frame, byte[] resultArray, int offset);
    }
}
