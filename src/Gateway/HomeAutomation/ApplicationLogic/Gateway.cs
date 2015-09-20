using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.Mqtt;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.Messaging;
using MosziNet.HomeAutomation.ApplicationLogic.Messages;

namespace MosziNet.HomeAutomation.BusinessLogic
{
    public class Gateway
    {
        XBeeService xbeeService;
        IMessageBus messageBus;
        MqttService mqttService;

        public Gateway(XBeeService xbeeService, MqttService mqttService)
        {
            this.xbeeService = xbeeService;
            this.mqttService = mqttService;

            messageBus = (IMessageBus)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(IMessageBus));

            // subscribe to the messages coming from that network.
            xbeeService.MessageReceived += xbeeService_MessageReceived;
            mqttService.MessageReceived += mqttService_MessageReceived;

            // signal that we are interested on the following topics
            mqttService.SubscribeTopic(MqttTopic.CommandTopic);
        }

        void mqttService_MessageReceived(string topicName, string message)
        {
            // todo
        }

        void xbeeService_MessageReceived(XBee.Frame.IXBeeFrame frame)
        {
            // put the frame on the message bus for later processing.
            messageBus.PostMessage(new XBeeFrameReceivedMessage(frame));
        }
    }
}
