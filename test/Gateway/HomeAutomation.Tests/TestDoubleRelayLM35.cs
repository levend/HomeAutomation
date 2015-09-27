using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MosziNet.HomeAutomation.Device.Concrete;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation;
using MosziNet.HomeAutomation.ApplicationLogic.Messages;

namespace HomeAutomation.Tests
{
    [TestClass]
    public class TestDoubleRelayLM35
    {
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

            MqttMessageReceived message = new MqttMessageReceived("/Command", "01,SetRelayState,1,1");
            message.ProcessMessage();
        }
    }
}
