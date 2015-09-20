using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation
{
    public class MqttTopic
    {
        public static readonly string StatusTopic = "/Status";

        public static readonly string LogTopic = "/Log";

        public static readonly string ErrorTopic = "/Error";

        public static readonly string CommandTopic = "/Command";
    }
}
