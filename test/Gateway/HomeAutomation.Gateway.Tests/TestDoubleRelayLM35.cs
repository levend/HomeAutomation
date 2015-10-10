using HomeAutomation.Core;
using HomeAutomation.DeviceNetwork.XBee.Device;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.XBee.Frame;
using System;

namespace HomeAutomation.Tests
{
    [TestClass]
    public class TestDoubleRelayLM35
    {

        public class MockXBeeService : IXBeeService
        {
            public event EventHandler<IXBeeFrame> MessageReceived;

            public void ProcessXBeeMessages()
            {
                if (this.MessageReceived != null)
                    this.MessageReceived(null, null);
            }

            public void SendFrame(MosziNet.HomeAutomation.XBee.Frame.IXBeeFrame frame)
            {

            }
        }

        [TestMethod]
        public void TestSwitchStates()
        {
            byte[] deviceId = new byte[] { 0x01 };

            DoubleRelayLM35 device = new DoubleRelayLM35();
            device.DeviceID = deviceId;
            device.NetworkAddress = new byte[] { 1 };

            DeviceRegistry registry = new DeviceRegistry();

            registry.RegisterDevice(null, device, deviceId);

            // TODO
        }
    }
}
