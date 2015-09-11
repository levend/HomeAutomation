using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee
{
    public interface IXbeeFrameSerializable
    {
        void Deserialize(byte[] buffer);

        byte[] Serialize();
    }
}
