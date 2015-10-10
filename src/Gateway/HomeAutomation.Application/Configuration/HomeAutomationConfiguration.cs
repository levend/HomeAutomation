using HomeAutomation.Communication.Mqtt;
using MosziNet.HomeAutomation.Gateway.Configuration;

namespace HomeAutomation.Application.Configuration
{
    public class HomeAutomationConfiguration
    {
        public XBeeConfiguration XBee { get; set; }

        public MqttServerConfiguration Mqtt { get; set; }

        public GatewayConfiguration Gateway { get; set; }
    }
}
