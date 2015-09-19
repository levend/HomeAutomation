using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.Mqtt;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.ApplicationLogic.Messages;

namespace MosziNet.HomeAutomation.BusinessLogic
{
    public class Gateway
    {
        XBeeService xbeeService;
        IMessageBus messageBus;

        public Gateway()
        {
            xbeeService = (XBeeService)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(XBeeService));
            messageBus = (IMessageBus)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(IMessageBus));

            // subscribe to the messages coming from that network.
            xbeeService.MessageReceived += xbeeService_MessageReceived;
        }

        void xbeeService_MessageReceived(XBee.Frame.IXBeeFrame frame)
        {
            // put the frame on the message bus for later processing.
            messageBus.PostMessage(new DeviceNotificationMessage(frame));
        }
    }
}
