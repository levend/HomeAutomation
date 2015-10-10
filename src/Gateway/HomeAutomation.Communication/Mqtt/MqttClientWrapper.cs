using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace HomeAutomation.Communication.Mqtt
{
    public class MqttClientWrapper : IMqttClient
    {
        MqttClient mqttClient;

        public MqttClientWrapper(string brokerHostName)
        {
            mqttClient = new MqttClient(brokerHostName);

            mqttClient.ConnectionClosed += MqttClient_ConnectionClosed;
            mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
        }
        public event Action<object, EventArgs> ConnectionClosed;
        public event Action<object, MqttMsgPublishEventArgs> MqttMsgPublishReceived;

        public bool IsConnected
        {
            get
            {
                return mqttClient.IsConnected;
            }
        }        

        public void Connect(string clientName)
        {
            mqttClient.Connect(clientName);
        }

        public void Publish(string topic, byte[] v)
        {
            mqttClient.Publish(topic, v);
        }

        public void Subscribe(string[] topics, byte[] qosLevels)
        {
            mqttClient.Subscribe(topics, qosLevels);
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            MqttMsgPublishReceived?.Invoke(sender, e);
        }

        private void MqttClient_ConnectionClosed(object sender, EventArgs e)
        {
            ConnectionClosed?.Invoke(sender, e);
        } 
    }
}
