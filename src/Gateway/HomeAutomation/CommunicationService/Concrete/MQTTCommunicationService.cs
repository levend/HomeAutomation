using System;
using Microsoft.SPOT;
using uPLibrary.Networking.M2Mqtt;
using System.Threading;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;

namespace MosziNet.HomeAutomation.CommunicationService
{
    public class MqttCommunicationService : ICommunicationService
    {
        private const int MinimumKeepAliveInterval = 15;

        // the connection and it's lock object
        private MqttClient mqttClient;
        private object mqttClientLockObject = new Object();

        public IMqttServerConfiguration configuration { get; private set; }
        private bool shouldMqttConnectionBeAlive;

        public MqttCommunicationService(IMqttServerConfiguration configuration)
        {
            if (configuration.ServerHostName == null || configuration.ServerHostName.Length == 0)
                throw new ArgumentOutOfRangeException("configuration", "ServerHostName should not be empty.");

            if (configuration.KeepAliveCheckPeriodInSeconds < MinimumKeepAliveInterval)
                throw new ArgumentOutOfRangeException("configuration", "Keep alive interval should be at least 15 seconds.");

            this.configuration = configuration;

            shouldMqttConnectionBeAlive = true;
            new Thread(MqttConnectionThread).Start();
        }


        public void SendMessage(string destinationId, string message)
        {
            
        }

        private void MQTTMessageReceived(string topicId, string message)
        {
            if (message == null || message.Length == 0)
                return;


        }


        /// <summary>
        /// Makes sure that the Mqtt server is always connected.
        /// </summary>
        private void MqttConnectionThread()
        {
            while (shouldMqttConnectionBeAlive)
            {
                lock(mqttClientLockObject)
                {
                    EnsureMqttServerIsConnected();
                }

                Thread.Sleep(configuration.KeepAliveCheckPeriodInSeconds * 1000);
            }
        }

        private void EnsureMqttServerIsConnected()
        {
            lock (mqttClientLockObject)
            {
                if (mqttClient == null || !mqttClient.IsConnected)
                {
                    try
                    {
                        mqttClient = new MqttClient(configuration.ServerHostName);

                        mqttClient.ConnectionClosed += mqttClient_ConnectionClosed;
                        mqttClient.MqttMsgPublishReceived += mqttClient_MqttMsgPublishReceived;

                        mqttClient.Connect(configuration.ClientName);

                        // subscribe to the topics on which we listen for messages
                        mqttClient.Subscribe(new string[] { configuration.TopicRootName + "/Status" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                    }
                    catch(Exception ex)
                    {
                        Debug.Print("Connection to MQTT server was not successful. Message: " + ex.Message);
                    }
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
