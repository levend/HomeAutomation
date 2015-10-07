using System;
using MosziNet.HomeAutomation.Device.Concrete;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation;
using MosziNet.HomeAutomation.ApplicationLogic.Messages;
using MosziNet.HomeAutomation.XBee;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace HomeAutomation.Tests
{
    [TestClass]
    public class TestDoubleRelayLM35
    {

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
        public void TestSwitchStates()
        {
            byte[] deviceId = new byte[] { 0x01 };

            DoubleRelayLM35 device = new DoubleRelayLM35();
            device.DeviceID = deviceId;
            device.NetworkAddress = new byte[] { 1 };

            DeviceRegistry registry = new DeviceRegistry();

            registry.RegisterDevice(device, deviceId);

            ApplicationContext.ServiceRegistry.RegisterService(typeof(DeviceRegistry), registry);
            ApplicationContext.ServiceRegistry.RegisterService(typeof(IXBeeService), new MockXBeeService());

            MqttMessageReceived message = new MqttMessageReceived("/Command", "01,SetRelayState,1,1");
            message.ProcessMessage();
        }
    }
}
