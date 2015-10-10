using HomeAutomation.Communication.Mqtt;

namespace HomeAutomation.Application.Factory
{
    public interface IMqttClientFactory
    {
        IMqttClient Create(MqttServerConfiguration configuration);
    }
}
