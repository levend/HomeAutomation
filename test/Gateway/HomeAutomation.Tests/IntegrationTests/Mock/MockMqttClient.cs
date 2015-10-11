using HomeAutomation.Communication.Mqtt;
using System;
using System.Collections.Generic;

namespace HomeAutomation.Tests.IntegrationTests
{
    public class MockMqttClient : IMqttClient
    {
        List<MqttMessage> messagesPublished = new List<MqttMessage>();

        bool isConnected;

        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
        }

        public event Action<object, EventArgs> ConnectionClosed;
        public event Action<object, MqttMessage> MqttMsgPublishReceived;

        public void Connect(string clientName)
        {
            isConnected = true;
        }

        public void Publish(string topic, string message)
        {
            messagesPublished.Add(new MqttMessage() { TopicName = topic, Message = message });
        }

        public void Subscribe(string[] v1, byte[] v2)
        {   

        }

        public List<MqttMessage> FlushSentMessages()
        {
            List<MqttMessage> returnList = new List<MqttMessage>(messagesPublished);

            messagesPublished.Clear();

            return returnList;
        }

    }
}
