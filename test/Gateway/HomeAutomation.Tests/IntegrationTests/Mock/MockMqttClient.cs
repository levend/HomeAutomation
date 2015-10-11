using HomeAutomation.Communication.Mqtt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.Tests.IntegrationTests
{
    public class MockMqttClient : IMqttClient
    {
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

        public void Publish(string topic, byte[] v)
        {
        }

        public void Subscribe(string[] v1, byte[] v2)
        {   
        }
    }
}
