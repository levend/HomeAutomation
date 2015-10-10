using HomeAutomation.Communication.Mqtt;

namespace HomeAutomation.Application.Factory
{
    internal class MqttClientFactory : IMqttClientFactory
    {
        public IMqttClient Create(MqttServerConfiguration configuration)
        {
            return new MqttClientWrapper(configuration.ServerHostName);
        }
    }
}
