using System;

namespace MosziNet.HomeAutomation.CommunicationService
{
    public interface IMqttServerConfiguration
    {
        string ServerHostName { get; }

        int KeepAliveCheckPeriodInSeconds { get; }

        string ClientName { get; }

        string TopicRootName { get; }
    }
}
