using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame;

namespace MosziNet.HomeAutomation.BusinessLogic.Messages
{
    public class DeviceCommandMessage : Message
    {
        IXBeeFrame commandFrame;

        public DeviceCommandMessage(IXBeeFrame frame)
        {
            commandFrame = frame;
            MessageType = ApplicationsConstants.MessageType_XBeeDeviceCommand;
        }

        public IXBeeFrame Frame
        {
            get { return commandFrame; }
        }
    }
}
