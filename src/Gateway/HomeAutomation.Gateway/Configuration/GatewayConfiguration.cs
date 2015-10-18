using System.Runtime.Serialization;

namespace HomeAutomation.Gateway.Configuration
{
    [DataContract]
    public class GatewayConfiguration
    {
        [DataMember]
        public int WatchdogPeriodInSeconds { get; set; }

        [DataMember]
        public int StatisticsAnnouncementPeriodInSeconds { get; set; }
    }
}
