using System;
using MosziNet.HomeAutomation.Gateway.Mqtt;
using MosziNet.HomeAutomation.Gateway.Messaging;

namespace MosziNet.HomeAutomation.Gateway.ApplicationLogic.Messages
{
    public class SendMessageToMqtt : IProcessableMessage
    {
        public string Message { get; set; }

        public string TopicSuffix { get; set; }

        public void ProcessMessage()
        {
            MqttService mqttService = (MqttService)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(MqttService));
            mqttService.SendMessage(mqttService.GetFullTopicName(TopicSuffix), Message);
        }
    }
}
