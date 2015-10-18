using HomeAutomation.Core;
using HomeAutomation.Core.Network;
using HomeAutomation.DeviceNetwork.XBee;
using MosziNet.XBee;
using System.Collections.Generic;

namespace HomeAutomation.Tests.IntegrationTests.Factory
{
    class MockXBeeDeviceNetworkFactory : IDeviceNetworkFactory
    {
        public static IXBeeSerialPort SerialPort = new MockXBeeSerialPort();

        public static MockXBeeDeviceNetworkFactory Instance { get; private set; } = new MockXBeeDeviceNetworkFactory();

        public IDeviceNetwork CreateDeviceNetwork(Dictionary<string, object> configuration)
        {
            return new XBeeDeviceNetwork(SerialPort);
        }
    }
}
