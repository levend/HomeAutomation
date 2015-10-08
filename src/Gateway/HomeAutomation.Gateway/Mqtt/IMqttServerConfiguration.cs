using System;

namespace MosziNet.HomeAutomation.Gateway.Mqtt
{
    public interface IMqttServerConfiguration
    {
        string ServerHostName { get; }

        int KeepAliveCheckPeriodInSeconds { get; }

        string ClientName { get; }

        string TopicRootName { get; }
    }
}
