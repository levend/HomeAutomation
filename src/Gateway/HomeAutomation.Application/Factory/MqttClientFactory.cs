using HomeAutomation.Communication.Mqtt;
using System.Runtime.Serialization;

namespace HomeAutomation.Application.Factory
{
    [DataContract]
    public class MqttClientFactory : IMqttClientFactory
    {
        public IMqttClient Create(MqttServerConfiguration configuration)
        {
            return new MqttClientWrapper(configuration.ServerHostName);
        }
    }
}
