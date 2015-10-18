using HomeAutomation.Communication.Mqtt;
using HomeAutomation.Core;
using HomeAutomation.Core.Controller;
using System;
using System.Collections.Generic;

namespace HomeAutomation.Controller.Mqtt
{
    public class MqttControllerFactory : IControllerFactory
    {
        public IHomeController CreateController(Dictionary<string, object> configuration)
        {
            IMqttClient client = new MqttClientWrapper((string)configuration["ServerHostName"]);

            MqttServerConfiguration mqttConfig = new MqttServerConfiguration()
            {
                ClientName = (string)configuration["ClientName"],
                ServerHostName = (string)configuration["ServerHostName"],
                KeepAliveCheckPeriodInSeconds = (int)configuration["KeepAliveCheckPeriodInSeconds"],
                TopicRootName = (string)configuration["TopicRootName"]
            };

            MqttService service = new MqttService(mqttConfig, client);
            MqttController controller = new MqttController(service);

            return controller;
        }
    }
}
