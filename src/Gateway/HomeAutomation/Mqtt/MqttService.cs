using System;
using Microsoft.SPOT;
using uPLibrary.Networking.M2Mqtt;
using System.Threading;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;
using System.Net;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.Mqtt
{
    public delegate void MessageReceivedDelegate(string topicName, string message);

    public class MqttService : IRunLoopParticipant
    {
        private const int MinimumKeepAliveInterval = 15;

        // the connection and it's lock object
        private MqttClient mqttClient;

        private IMqttServerConfiguration configuration { get; set; }

        public event MessageReceivedDelegate MessageReceived;

        public MqttService(IMqttServerConfiguration config)
        {
            if (config.ServerHostName == null || config.ServerHostName.Length == 0)
                throw new ArgumentOutOfRangeException("configuration", "ServerHostName should not be empty.");

            if (config.KeepAliveCheckPeriodInSeconds < MinimumKeepAliveInterval)
                throw new ArgumentOutOfRangeException("configuration", "Keep alive interval should be at least 15 seconds.");

            if (config.TopicRootName[config.TopicRootName.Length - 1] == '/')
                throw new ArgumentOutOfRangeException("configuration", "Topic name should not have a trailing / character.");

            this.configuration = config;

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
            mqttClient.Publish(topic, Encoding.UTF8.GetBytes(message));
        }

        public void SubscribeTopic(string topic)
        {
            mqttClient.Subscribe(new string[] { GetFullTopicName(topic) }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
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


        private void MQTTMessageReceived(string topicId, string message)
        {
            if (message == null || message.Length == 0)
                return;

            // strip down the topic root name
            topicId = GetTopicNameSuffix(topicId);

            MessageReceivedDelegate mrd = this.MessageReceived;
            if (mrd != null)
            {
                mrd(topicId, message);
            }

            Log.Information("[" + topicId + "] " + message);
        }

        private void EnsureMqttServerIsConnected()
        {
            if (mqttClient == null || !mqttClient.IsConnected)
            {
                try
                {
                    mqttClient = new MqttClient(configuration.ServerHostName);

                    mqttClient.ConnectionClosed += mqttClient_ConnectionClosed;
                    mqttClient.MqttMsgPublishReceived += mqttClient_MqttMsgPublishReceived;

                    mqttClient.Connect(configuration.ClientName);
                }
                catch(Exception ex)
                {
                    Log.Debug("Connection to MQTT server was not successful. \n" + ExceptionFormatter.Format(ex));
                }
            }
        }

        void mqttClient_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            String message = new String(Encoding.UTF8.GetChars(e.Message));
            
            MQTTMessageReceived(e.Topic, message);
        }

        void mqttClient_ConnectionClosed(object sender, EventArgs e)
        {
            // do nothing, the connection thread will make sure the connection is kept alive
        }

        public void Execute()
        {
            EnsureMqttServerIsConnected();
        }
    }
}
