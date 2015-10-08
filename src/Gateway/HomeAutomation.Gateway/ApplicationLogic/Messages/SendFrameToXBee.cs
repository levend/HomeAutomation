using System;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.Gateway.Messaging;

namespace MosziNet.HomeAutomation.Gateway.ApplicationLogic.Messages
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
