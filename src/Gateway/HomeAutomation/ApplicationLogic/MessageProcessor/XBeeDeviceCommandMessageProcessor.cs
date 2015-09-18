using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.ApplicationLogic.Messages;

namespace MosziNet.HomeAutomation.ApplicationLogic.MessageProcessor
{
    public class XBeeDeviceCommandMessageProcessor : IMessageProcessor
    {
        public void ProcessMessage(Message message)
        {
            XBeeService xbeeService = (XBeeService)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(XBeeService));

            xbeeService.SendFrame(((DeviceCommandMessage)message).Frame);
        }
    }
}
