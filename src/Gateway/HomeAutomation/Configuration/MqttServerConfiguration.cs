using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Mqtt;

namespace MosziNet.HomeAutomation.Configuration
{
    /// <summary>
    /// Contains configuration about the MQTT server.
    /// </summary>
    public class MqttServerConfiguration : IMqttServerConfiguration
    {
        /// <summary>
        /// Can contain IP address or host name.
        /// </summary>
        public string ServerHostName { get; private set; }

        /// <summary>
        /// The number of seconds the keep alive loop should wait between keep alive checks.
        /// </summary>
        public int KeepAliveCheckPeriodInSeconds { get; private set; }

        /// <summary>
        /// The client name how this application presents itself to the MQTT server.
        /// </summary>
        public string ClientName { get; private set; }

        /// <summary>
        /// The root of the topic on which messages will be published and received.
        /// </summary>
        public string TopicRootName { get; private set; }

        public MqttServerConfiguration(string serverHostName, int keepAlivePeriod, string clientName, string topicRootName)
        {
            ServerHostName = serverHostName;
            KeepAliveCheckPeriodInSeconds = keepAlivePeriod;
            ClientName = clientName;
            TopicRootName = topicRootName;
        }
    }
}
