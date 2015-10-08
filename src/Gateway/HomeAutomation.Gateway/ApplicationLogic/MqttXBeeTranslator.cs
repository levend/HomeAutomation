using MosziNet.HomeAutomation.Gateway.ApplicationLogic.Messages;
using MosziNet.HomeAutomation.Gateway.Mqtt;
using MosziNet.HomeAutomation.Gateway.Messaging;
using MosziNet.HomeAutomation.XBee;

namespace MosziNet.HomeAutomation.Gateway.BusinessLogic
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

        private void mqttService_MessageReceived(object sender, MqttMessage e)
        {
            // when we received a message from MQTT we will immediatelly put it on the bus for later processing
            messageBus.PostMessage(new MqttMessageReceived(e.TopicName, e.Message));
        }

        private void xbeeService_MessageReceived(object sender, XBee.Frame.IXBeeFrame e)
        {
            // put the frame on the message bus for later processing.
            messageBus.PostMessage(new XBeeFrameReceivedMessage(e));
        }
    }
}
