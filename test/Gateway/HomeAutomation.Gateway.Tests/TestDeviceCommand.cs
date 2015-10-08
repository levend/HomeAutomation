using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MosziNet.HomeAutomation;
using MosziNet.HomeAutomation.Gateway.Device;
using MosziNet.HomeAutomation.Gateway.Device.Base;
using MosziNet.HomeAutomation.Gateway.Mqtt;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.Gateway.ApplicationLogic.Messages;

namespace HomeAutomation.Tests
{
    [TestClass]
    public class TestDeviceCommand
    {
        public class MockTestDevice : DeviceBase
        {
            public bool TestSuccess { get; set; }

            public override void ProcessFrame(MosziNet.HomeAutomation.XBee.Frame.IXBeeFrame frame)
            {
                
            }

            public override DeviceState GetDeviceState()
            {
                return null;
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

        [TestInitialize]
        public void Setup()
        {
            ApplicationContext.ServiceRegistry = new MosziNet.HomeAutomation.Gateway.Service.ServiceRegistry();
        }

        [TestMethod]
        public void TestSimpleCommand()
        {
            byte[] deviceId = new byte[] { 0x01 };

            MockTestDevice device = new MockTestDevice();
            device.DeviceID = deviceId;
            device.NetworkAddress = new byte[] { 1 };

            DeviceRegistry registry = new DeviceRegistry();

            registry.RegisterDevice(device, deviceId);

            ApplicationContext.ServiceRegistry.RegisterService(typeof(DeviceRegistry), registry);
            ApplicationContext.ServiceRegistry.RegisterService(typeof(IXBeeService), new MockXBeeService());

            MqttMessageReceived message = new MqttMessageReceived("/Command", "01,DoMyMethod,YES");
            message.ProcessMessage();

            Assert.IsTrue(device.TestSuccess);
        }
    }
}
