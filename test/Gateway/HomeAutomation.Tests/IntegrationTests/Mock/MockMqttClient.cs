﻿using HomeAutomation.Communication.Mqtt;
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
            set
            {
                isConnected = value;
                if (!isConnected)
                {
                    ConnectionClosed?.Invoke(this, null);
                }
            }
        }

        public event Action<object, EventArgs> ConnectionClosed;
        public event Action<object, MqttMessage> MqttMsgPublishReceived;

        public void Connect(string clientName, string username, string password)
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

        internal void GenerateMessageOnTopic(string topic, string message)
        {
            MqttMsgPublishReceived?.Invoke(this, new MqttMessage() { TopicName = topic, Message = message });
        }
    }
}
