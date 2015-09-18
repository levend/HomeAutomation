using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame;

namespace MosziNet.HomeAutomation.ApplicationLogic.MessageProcessor.XBeeFrameProcessor
{
    public interface IXBeeFrameProcessor
    {
        void ProcessFrame(IXBeeFrame frame);
    }
}
