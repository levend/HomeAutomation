using HomeAutomation.Application.Factory;
using HomeAutomation.Communication.Mqtt;
using HomeAutomation.Core;
using HomeAutomation.Gateway.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HomeAutomation.Application.Configuration
{
    [DataContract]
    public class HomeAutomationConfiguration
    {
        [DataMember]
        public GatewayConfiguration Gateway { get; set; }

        [DataMember]
        public List<DeviceNetworkConfiguration> DeviceNetworks { get; set; }

        [DataMember]
        public List<DeviceTypeDescription> DeviceTypes { get; set; }

        [DataMember]
        public List<ControllerConfiguration> Controllers { get; set; }
    }
}
