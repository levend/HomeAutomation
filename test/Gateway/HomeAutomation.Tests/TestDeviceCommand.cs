using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MosziNet.HomeAutomation.Device.Base;
using MosziNet.HomeAutomation;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation.ApplicationLogic.Messages;
using MosziNet.HomeAutomation.XBee;

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

            public override MosziNet.HomeAutomation.Device.DeviceState GetDeviceState()
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
            public event MessageReceivedDelegate MessageReceived;

            public void ProcessXBeeMessages()
            {
                if (this.MessageReceived != null)
                    this.MessageReceived(null);
            }

            public void SendFrame(MosziNet.HomeAutomation.XBee.Frame.IXBeeFrame frame)
            {
                
            }
        }

        [TestInitialize]
        public void Setup()
        {
            ApplicationContext.ServiceRegistry = new MosziNet.HomeAutomation.Service.ServiceRegistry();
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
