using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Messaging;

namespace MosziNet.HomeAutomation.ApplicationLogic.Messages
{
    public class MqttMessageReceived : IProcessableMessage
    {
        private string topic;
        private string message;

        public MqttMessageReceived(string topicName, string messageContent)
        {
            topic = topicName;
            message = messageContent;
        }

        public void ProcessMessage()
        {
            
        }
    }
}
