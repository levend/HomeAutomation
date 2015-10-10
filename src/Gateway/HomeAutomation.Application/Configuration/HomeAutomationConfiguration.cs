using HomeAutomation.Application.Factory;
using HomeAutomation.Communication.Mqtt;
using HomeAutomation.Gateway.Configuration;

namespace HomeAutomation.Application.Configuration
{
    public class HomeAutomationConfiguration
    {
        public XBeeConfiguration XBee { get; set; }

        public MqttServerConfiguration Mqtt { get; set; }

        public GatewayConfiguration Gateway { get; set; }

        // Factory interfaces
        public IMqttClientFactory MqttClientFactory { get; set; }

        public IXBeeSerialPortFactory XBeeSerialPortFactory { get; set; }
    }
}
