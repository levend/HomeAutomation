using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame;
using System.Collections;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using MosziNet.HomeAutomation.ApplicationLogic.XBeeFrameProcessor;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Messaging;

namespace MosziNet.HomeAutomation.ApplicationLogic.Messages
{
    public class XBeeFrameReceivedMessage : IProcessableMessage
    {
        private static Hashtable frameProcessors = new Hashtable();

        static XBeeFrameReceivedMessage()
        {
            frameProcessors.Add(typeof(RemoteCommandResponse), new RemoteCommandResponseProcessor());
            frameProcessors.Add(typeof(IODataSample), new IODataSampleFrameProcessor());
        }

        public IXBeeFrame Frame { get; set; }

        public XBeeFrameReceivedMessage(IXBeeFrame receivedFrame)
        {
            Frame = receivedFrame;
        }

        public void ProcessMessage()
        {
            Type frameType = Frame.GetType();
            IXBeeFrameProcessor processor = frameProcessors.Contains(frameType) ? (IXBeeFrameProcessor)frameProcessors[frameType] : null;

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
