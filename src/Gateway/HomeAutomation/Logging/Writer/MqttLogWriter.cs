using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.Logging.Writer
{
    public class MqttLogWriter : ILogWriter
    {
        private Mqtt.MqttService mqttService;
        private string logTopic;
        ILogFormatter logFormatter;

        public MqttLogWriter(Mqtt.MqttService mqttServiceInstance, string logTopicName, ILogFormatter logFormatterInstance)
        {
            // todo: logformatters should be specificed in the Log class, not here
            logTopic = logTopicName;
            mqttService = mqttServiceInstance;
            logFormatter = logFormatterInstance;
        }

        public void Log(string message, LogLevel logLevel)
        {
            mqttService.SendMessage(logTopic, logFormatter.Format(message, logLevel));
        }
    }
}
