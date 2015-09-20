using System;
using Microsoft.SPOT;
using uPLibrary.Networking.M2Mqtt;
using System.Threading;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;
using System.Net;

namespace MosziNet.HomeAutomation.Mqtt
{
    public class MqttService
    {
        private const int MinimumKeepAliveInterval = 15;

        // the connection and it's lock object
        private MqttClient mqttClient;

        public IMqttServerConfiguration configuration { get; private set; }
        private bool shouldMqttConnectionBeAlive;

        public MqttService(IMqttServerConfiguration config)
        {
            if (config.ServerHostName == null || config.ServerHostName.Length == 0)
                throw new ArgumentOutOfRangeException("configuration", "ServerHostName should not be empty.");

            if (config.KeepAliveCheckPeriodInSeconds < MinimumKeepAliveInterval)
                throw new ArgumentOutOfRangeException("configuration", "Keep alive interval should be at least 15 seconds.");

            this.configuration = config;

            shouldMqttConnectionBeAlive = true;
            new Thread(MqttConnectionThread).Start();
        }

        public void SendMessage(string topic, string message)
        {
            mqttClient.Publish(topic, Encoding.UTF8.GetBytes(message));
        }

        private void MQTTMessageReceived(string topicId, string message)
        {
            if (message == null || message.Length == 0)
                return;

            Debug.Print("[" + topicId + "] " + message);
        }

        /// <summary>
        /// Makes sure that the Mqtt server is always connected.
        /// </summary>
        private void MqttConnectionThread()
        {
            while (shouldMqttConnectionBeAlive)
            {
                EnsureMqttServerIsConnected();
            }
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

                    // subscribe to the topics on which we listen for messages
                    mqttClient.Subscribe(new string[] { configuration.TopicRootName + "/Command" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

                    mqttClient.Connect(configuration.ClientName);
                }
                catch(Exception ex)
                {
                    Debug.Print("Connection to MQTT server was not successful. \n" + ex);
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
    }
}
