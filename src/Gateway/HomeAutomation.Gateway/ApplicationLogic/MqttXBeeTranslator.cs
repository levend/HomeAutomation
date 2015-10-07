using System;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.Mqtt;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.Messaging;
using MosziNet.HomeAutomation.ApplicationLogic.Messages;

namespace MosziNet.HomeAutomation.BusinessLogic
{
    public class MqttXBeeTranslator
    {
        IXBeeService xbeeService;
        IMessageBus messageBus;
        MqttService mqttService;

        public MqttXBeeTranslator(IXBeeService xbeeService, MqttService mqttService, IMessageBus messageBus)
        {
            this.xbeeService = xbeeService;
            this.mqttService = mqttService;

            this.messageBus = messageBus;

            // subscribe to the messages coming from that network.
            xbeeService.MessageReceived += xbeeService_MessageReceived;
            mqttService.MessageReceived += mqttService_MessageReceived;

            // signal that we are interested on the following topics
            mqttService.SubscribeTopic(MqttTopic.CommandTopic);
        }

        void mqttService_MessageReceived(string topicName, string message)
        {
            // when we received a message from MQTT we will immediatelly put it on the bus for later processing
            messageBus.PostMessage(new MqttMessageReceived(topicName, message));
        }

        void xbeeService_MessageReceived(XBee.Frame.IXBeeFrame frame)
        {
            // put the frame on the message bus for later processing.
            messageBus.PostMessage(new XBeeFrameReceivedMessage(frame));
        }
    }
}
