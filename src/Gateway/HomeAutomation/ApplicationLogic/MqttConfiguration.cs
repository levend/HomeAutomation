using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.ApplicationLogic
{
    public class MqttConfiguration
    {
        public static readonly string StatusTopic = "/Status";

        public static readonly string LogTopic = "/Log";

        public static readonly string ErrorTopic = "/Error";

        public static readonly string CommandTopic = "/Command";
    }
}
