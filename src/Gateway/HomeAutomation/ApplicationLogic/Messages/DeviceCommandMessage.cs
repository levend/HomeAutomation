using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame;

namespace MosziNet.HomeAutomation.ApplicationLogic.Messages
{
    public class DeviceCommandMessage : Message
    {
        IXBeeFrame commandFrame;

        public DeviceCommandMessage(IXBeeFrame frame)
        {
            commandFrame = frame;
        }

        public IXBeeFrame Frame
        {
            get { return commandFrame; }
        }
    }
}
