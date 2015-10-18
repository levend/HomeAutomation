using HomeAutomation.Communication.Mqtt;
using System.Runtime.Serialization;

namespace HomeAutomation.Application.Factory
{
    public interface IMqttClientFactory
    {
        IMqttClient Create(MqttServerConfiguration configuration);
    }
}
