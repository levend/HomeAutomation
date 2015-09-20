using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.Messaging;

namespace MosziNet.HomeAutomation.ApplicationLogic.Messages
{
    public class DeviceCommandMessage : IProcessableMessage
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

        public void ProcessMessage()
        {
            XBeeService xbeeService = (XBeeService)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(XBeeService));

            xbeeService.SendFrame(commandFrame);
        }
    }
}
