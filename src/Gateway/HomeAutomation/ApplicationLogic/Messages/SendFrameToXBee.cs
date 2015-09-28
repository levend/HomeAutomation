using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.Messaging;

namespace MosziNet.HomeAutomation.ApplicationLogic.Messages
{
    public class SendFrameToXBee : IProcessableMessage
    {
        IXBeeFrame commandFrame;

        public SendFrameToXBee(IXBeeFrame frame)
        {
            commandFrame = frame;
        }

        public IXBeeFrame Frame
        {
            get { return commandFrame; }
        }

        public void ProcessMessage()
        {
            IXBeeService xbeeService = (IXBeeService)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(IXBeeService));

            xbeeService.SendFrame(commandFrame);
        }
    }
}
