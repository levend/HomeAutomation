using HomeAutomation.Core.Service;
using HomeAutomation.Logging;
using HomeAutomation.Util;
using System;
using System.Collections.Generic;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace HomeAutomation.Communication.Mqtt
{
    public class MqttService : ICooperativeService
    {
        private List<string> subscribedTopics = new List<string>();

        // the connection and it's lock object
        private IMqttClient mqttClient;

        private MqttServerConfiguration configuration;

        public event EventHandler<MqttMessage> MessageReceived;

        public MqttService(MqttServerConfiguration config, IMqttClient mqttClient)
        {
            this.configuration = config;
            this.mqttClient = mqttClient;

            // start the MQTT client immediately.
            EnsureMqttServerIsConnected();
        }

        /// <summary>
        /// Sends a message to the MQTT bus.
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="message"></param>
        public void SendMessage(string topic, string message)
        {
            mqttClient.Publish(topic, message);
        }

        public void SubscribeTopic(string topic)
        {
            string fullTopicName = GetFullTopicName(topic);
            if (!subscribedTopics.Contains(fullTopicName))
            {
                subscribedTopics.Add(fullTopicName);
            }

            mqttClient.Subscribe(new string[] { fullTopicName }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
        }

        /// <summary>
        /// Returns the full topic name by concatenating the topic name suffix to the root topic name.
        /// </summary>
        /// <param name="topicNameSuffix"></param>
        /// <returns></returns>
        public string GetFullTopicName(string topicNameSuffix)
        {
            if (topicNameSuffix[0] != '/')
                throw new ArgumentOutOfRangeException("topicNameSuffix", "Topic name suffix should begin with a / character.");

            return configuration.TopicRootName + topicNameSuffix;
        }

        /// <summary>
        /// Returns the subtopic name
        /// </summary>
        /// <param name="fullTopicName"></param>
        /// <returns></returns>
        public string GetTopicNameSuffix(string fullTopicName)
        {
            return fullTopicName.Substring(configuration.TopicRootName.Length);
        }

        private void EnsureMqttServerIsConnected()
        {
            if (mqttClient == null || !mqttClient.IsConnected)
            {
                try
                {
                    //mqttClient = mqttClientFactory.Create(configuration);

                    mqttClient.ConnectionClosed += mqttClient_ConnectionClosed;
                    mqttClient.MqttMsgPublishReceived += mqttClient_MqttMsgPublishReceived;

                    mqttClient.Connect(configuration.ClientName);

                    //// ensure that we re-connect any subscribed topics
                    //for (int i = 0; i < subscribedTopics.Count; i++)
                    //{
                    //    SubscribeTopic(subscribedTopics[i]);
                    //}
                }
                catch(Exception ex)
                {
                    Log.Debug("Connection to MQTT server could not be established. Exception: \n" + ex.FormatToLog());
                }
            }
        }

        private void mqttClient_MqttMsgPublishReceived(object arg1, MqttMessage message)
        {
            // fix the topic name to reflect only the suffix
            message.TopicName = GetTopicNameSuffix(message.TopicName);

            // bubble up the event
            MessageReceived?.Invoke(this, message);

            Log.Information("[" + message.TopicName + "] " + message);
        }

        void mqttClient_ConnectionClosed(object sender, EventArgs e)
        {
            // do nothing, the connection watcher task will make sure the connection is kept alive
        }

        public void ExecuteTasks()
        {
            EnsureMqttServerIsConnected();
        }
    }
}
