using System;
using MosziNet.HomeAutomation.XBee.Frame;
using System.Collections;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using MosziNet.HomeAutomation.Gateway.ApplicationLogic.XBeeFrameProcessor;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Gateway.Messaging;
using MosziNet.HomeAutomation.Gateway.Configuration;

namespace MosziNet.HomeAutomation.Gateway.ApplicationLogic.Messages
{
    public class XBeeFrameReceivedMessage : IProcessableMessage
    {
        public IXBeeFrame Frame { get; set; }

        public XBeeFrameReceivedMessage(IXBeeFrame receivedFrame)
        {
            Frame = receivedFrame;
        }

        public void ProcessMessage()
        {
            Type frameType = Frame.GetType();
            IXBeeFrameProcessor processor = ApplicationContext.Configuration.GetObjectForKey(ApplicationConfigurationCategory.XBeeFrameProcessor, frameType) as IXBeeFrameProcessor;

            if (processor != null)
            {
                processor.ProcessFrame(Frame);
            }
            else
            {
                Log.Debug("Dropping frame with type " + frameType.Name + " as no suitable processor is found.");
            }
        }
    }
}
