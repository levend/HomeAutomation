namespace HomeAutomation.Communication.Mqtt
{
    /// <summary>
    /// Contains configuration about the MQTT server.
    /// </summary>
    public class MqttServerConfiguration
    {
        /// <summary>
        /// Can contain IP address or host name.
        /// </summary>
        public string ServerHostName { get; set; }

        /// <summary>
        /// The number of seconds the keep alive loop should wait between keep alive checks.
        /// </summary>
        public int KeepAliveCheckPeriodInSeconds { get; set; }

        /// <summary>
        /// The client name how this application presents itself to the MQTT server.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// The root of the topic on which messages will be published and received.
        /// </summary>
        public string TopicRootName { get; set; }
    }
}
