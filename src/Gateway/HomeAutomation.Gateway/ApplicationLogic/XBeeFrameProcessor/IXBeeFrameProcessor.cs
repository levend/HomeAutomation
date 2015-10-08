using System;
using MosziNet.HomeAutomation.XBee.Frame;

namespace MosziNet.HomeAutomation.Gateway.ApplicationLogic.XBeeFrameProcessor
{
    public interface IXBeeFrameProcessor
    {
        void ProcessFrame(IXBeeFrame frame);
    }
}
