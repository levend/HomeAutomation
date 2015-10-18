using System;
using HomeAutomation.Core;
using HomeAutomation.Communication.Mqtt;
using System.Collections.Generic;

namespace HomeAutomation.Controller.Mqtt
{
    public class MqttController : IController
    {
        MqttService mqttService;

        public MqttController(MqttService mqttService)
        {
            this.mqttService = mqttService;

            // subscribe to the messages coming from that network.
            mqttService.MessageReceived += MqttService_MessageReceived;

            // signal that we are interested on the following topics
            mqttService.SubscribeTopic(MqttTopic.CommandTopic);
        }

        public event EventHandler<DeviceCommand> DeviceCommandArrived;

        public void ExecuteTasks()
        {
            // nothing to do here ?
        }

        public void SendDeviceState(DeviceState deviceState)
        {
            mqttService.SendMessage(mqttService.GetFullTopicName(MqttTopic.StatusTopic), deviceState.ConvertToString());
        }

        public void SendGatewayHeartbeatMessage(string message)
        {
            mqttService.SendMessage(mqttService.GetFullTopicName(MqttTopic.Heartbeat), message);
        }

        public void SendStatistics(Dictionary<string, object> statisticValues)
        {
            mqttService.SendMessage(
                mqttService.GetFullTopicName(MqttTopic.Statistics),
                "[Statistics] XBee messages received: " + statisticValues["XBeeMessageReceiveCount"]);
        }

        private void MqttService_MessageReceived(object sender, MqttMessage e)
        {
            // if the message was coming through the / Admin topic then execute it with the AdminCommandDistributor
            if (e.TopicName == MqttTopic.Admin)
            {
                // todo
            }

            // if the message arrived through the /Command topic then send the command directly to the device that was targeted
            if (e.TopicName == MqttTopic.CommandTopic)
            {
                DeviceCommand command = DeviceCommand.CreateFromString(e.Message);

                DeviceCommandArrived?.Invoke(this, command);
            }
        }
    }
}
