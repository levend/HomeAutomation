using System;
using System.Collections.Generic;
using HomeAutomation.Application.Factory;
using HomeAutomation.Communication.Mqtt;
using HomeAutomation.Core;
using HomeAutomation.Core.Controller;
using HomeAutomation.Controller.Mqtt;

namespace HomeAutomation.Tests.IntegrationTests.Factory
{
    class MockMqttControllerFactory : IControllerFactory
    {
        public static MockMqttClient Client = new MockMqttClient();

        public static MockMqttControllerFactory Instance { get; private set; } = new MockMqttControllerFactory();

        public IHomeController CreateController(Dictionary<string, object> configuration)
        {
            MqttServerConfiguration mqttConfig = new MqttServerConfiguration()
            {
                ClientName = (string)configuration["ClientName"],
                ServerHostName = (string)configuration["ServerHostName"],
                KeepAliveCheckPeriodInSeconds = (int)configuration["KeepAliveCheckPeriodInSeconds"],
                TopicRootName = (string)configuration["TopicRootName"]
            };

            MqttService service = new MqttService(mqttConfig, Client);
            MqttController controller = new MqttController(service);

            return controller;
        }
    }
}
