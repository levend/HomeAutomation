using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.Logging.Writer
{
    public class MqttLogWriter : ILogWriter
    {
        private Mqtt.MqttService mqttService;
        private string logTopic;

        public MqttLogWriter(Mqtt.MqttService mqttServiceInstance, string logTopicName)
        {
            logTopic = logTopicName;
            mqttService = mqttServiceInstance;
        }

        public void Log(string message, LogLevel logLevel, ILogFormatter logFormatter)
        {
            mqttService.SendMessage(logTopic, logFormatter.Format(message, logLevel));
        }
    }
}
