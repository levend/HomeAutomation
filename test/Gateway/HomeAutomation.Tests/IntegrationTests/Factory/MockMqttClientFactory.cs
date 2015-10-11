using System;
using HomeAutomation.Application.Factory;
using HomeAutomation.Communication.Mqtt;

namespace HomeAutomation.Tests.IntegrationTests.Factory
{
    class MockMqttClientFactory : IMqttClientFactory
    {
        static MockMqttClient client = new MockMqttClient();

        public static MockMqttClientFactory Instance { get; private set; } = new MockMqttClientFactory();

        public IMqttClient Create(MqttServerConfiguration configuration)
        {
            return client;
        }

        public IMqttClient CreateNew(MqttServerConfiguration configuration)
        {
            return new MockMqttClient();
        }
    }
}
