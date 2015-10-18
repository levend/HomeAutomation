using HomeAutomation.Communication.Mqtt;
using HomeAutomation.Controller.Mqtt;
using HomeAutomation.Core;
using HomeAutomation.Core.Controller;
using System;
using System.Collections.Generic;

namespace HomeAutomation.Tests.IntegrationTests.Factory
{
    class MockMqttControllerFactory : IControllerFactory
    {
        public static MockMqttClient Client = new MockMqttClient();

        public static MockMqttControllerFactory Instance { get; private set; } = new MockMqttControllerFactory();

        public IController CreateController(Dictionary<string, string> configuration)
        {
            MqttServerConfiguration mqttConfig = new MqttServerConfiguration()
            {
                ClientName = configuration["ClientName"],
                ServerHostName = configuration["ServerHostName"],
                KeepAliveCheckPeriodInSeconds = Int32.Parse(configuration["KeepAliveCheckPeriodInSeconds"]),
                TopicRootName = configuration["TopicRootName"]
            };

            MqttService service = new MqttService(mqttConfig, Client);
            MqttController controller = new MqttController(service);

            return controller;
        }
    }
}
