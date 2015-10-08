using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosziNet.HomeAutomation.Gateway.Mqtt
{
    public class MqttMessage
    {
        public string TopicName { get; set; }

        public string Message { get; set; }
    }
}
