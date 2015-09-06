using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation
{
    public static class ApplicationConfiguration
    {
        public static readonly string MessageType_XBeeDeviceCommand = "MessageType_XBeeDeviceCommand";

        public static readonly string CommunicationServiceProvider_XBee = "CommunicationServiceProvider_XBee";
        public static readonly string CommunicationServiceProvider_MQTT = "CommunicationServiceProvider_MQTT";
    }
}
