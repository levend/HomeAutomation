using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame;

namespace MosziNet.HomeAutomation.ApplicationLogic.Messages
{
    public class DeviceNotificationMessage : Message
    {
        public IXBeeFrame Frame { get; set; }

        public DeviceNotificationMessage(IXBeeFrame receivedFrame)
        {
            Frame = receivedFrame;
        }
    }
}
