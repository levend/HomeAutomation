using System;
using HomeAutomation.Application.Factory;
using HomeAutomation.Communication.Mqtt;

namespace HomeAutomation.Tests.IntegrationTests.Factory
{
    class MockMqttClientFactory : IMqttClientFactory
    {
        public IMqttClient Create(MqttServerConfiguration configuration)
        {
            return new MockMqttClient();
        }
    }
}
