using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.BusinessLogic.Messages;

namespace MosziNet.HomeAutomation.ApplicationLogic.MessageProcessor
{
    public class XBeeCommandMessageProcessor : IMessageProcessor
    {
        public void ProcessMessage(Message message)
        {
            XBeeService xbeeService = (XBeeService)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(XBeeService));

            xbeeService.SendFrame(((DeviceCommandMessage)message).Frame);
        }
    }
}
