using HomeAutomation.Core;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.XBee.Frame;
using System;

namespace HomeAutomation.Tests
{
    [TestClass]
    public class TestDeviceCommand
    {
        public class MockTestDevice : DeviceBase
        {
            public bool TestSuccess { get; set; }

            public override DeviceState DeviceState
            {
                get
                {
                    return null;
                }
            }

            public void DoMyMethod(string param1)
            {
                TestSuccess = param1 == "YES";
            }
        }

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
        public void TestSimpleCommand()
        {
            byte[] deviceId = new byte[] { 0x01 };

            MockTestDevice device = new MockTestDevice();
            device.DeviceID = deviceId;
            device.NetworkAddress = new byte[] { 1 };

            DeviceRegistry registry = new DeviceRegistry();

            registry.RegisterDevice(null, device, deviceId);

            Assert.IsTrue(device.TestSuccess);
        }
    }
}
