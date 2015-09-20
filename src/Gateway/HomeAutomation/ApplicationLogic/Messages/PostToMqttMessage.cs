using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Mqtt;
using MosziNet.HomeAutomation.Messaging;

namespace MosziNet.HomeAutomation.ApplicationLogic.Messages
{
    public class PostToMqttMessage : IProcessableMessage
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
