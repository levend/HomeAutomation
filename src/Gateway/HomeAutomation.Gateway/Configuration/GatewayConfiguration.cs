using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.Gateway.Configuration
{
    public class GatewayConfiguration
    {
        public int WatchdogPeriodInSeconds { get; set; }

        public int StatisticsAnnouncementPeriodInSeconds { get; set; }
    }
}
