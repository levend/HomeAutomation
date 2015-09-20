using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.ApplicationLogic.Messages;
using MosziNet.HomeAutomation.ApplicationLogic.MessageProcessor.XBeeFrameProcessor;
using MosziNet.HomeAutomation.XBee.Frame;
using System.Collections;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using MosziNet.HomeAutomation.Logging;

namespace MosziNet.HomeAutomation.ApplicationLogic.MessageProcessor
{
    public class DeviceNotificationMessageProcessor : IMessageProcessor
    {
        private static Hashtable frameProcessors = new Hashtable();

        static DeviceNotificationMessageProcessor()
        {
            frameProcessors.Add(typeof(RemoteCommandResponse), new RemoteCommandResponseProcessor());
            frameProcessors.Add(typeof(IODataSample), new IODataSampleFrameProcessor());
        }

        public void ProcessMessage(Message message)
        {
            DeviceNotificationMessage notification = message as DeviceNotificationMessage;
            if (notification != null)
            {
                Type frameType = notification.Frame.GetType();
                IXBeeFrameProcessor processor = frameProcessors.Contains(frameType) ? (IXBeeFrameProcessor)frameProcessors[frameType] : null;

                if (processor != null)
                {
                    processor.ProcessFrame(notification.Frame);
                }
                else
                {
                    Log.Debug("Dropping frame with type " + frameType.Name + " as no suitable processor is found.");
                }
            }
        }
    }
}
