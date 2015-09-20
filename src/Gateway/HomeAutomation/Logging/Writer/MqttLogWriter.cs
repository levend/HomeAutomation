using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.Logging.Writer
{
    public class MqttLogWriter : ILogWriter
    {
        private Mqtt.MqttService mqttService;
        private string subTopic;

        public MqttLogWriter(Mqtt.MqttService mqttServiceInstance, string subTopicName)
        {
            subTopic = subTopicName;
            mqttService = mqttServiceInstance;
        }

        public void Log(string message, LogLevel logLevel, ILogFormatter logFormatter)
        {
            if (logLevel != LogLevel.Information)
            {
                mqttService.SendMessage(mqttService.GetFullTopicName(subTopic), logFormatter.Format(message, logLevel));
            }
        }
    }
}
