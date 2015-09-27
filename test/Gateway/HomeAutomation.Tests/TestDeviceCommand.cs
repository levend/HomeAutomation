using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MosziNet.HomeAutomation.Device.Base;
using MosziNet.HomeAutomation;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation.ApplicationLogic.Messages;

namespace HomeAutomation.Tests
{
    [TestClass]
    public class TestDeviceCommand
    {
        public class MyTestDevice : DeviceBase
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

        [TestMethod]
        public void TestSimpleCommand()
        {
            byte[] deviceId = new byte[] { 0x01 };

            MyTestDevice device = new MyTestDevice();
            device.DeviceID = deviceId;
            device.NetworkAddress = new byte[] { 1 };

            DeviceRegistry registry = new DeviceRegistry();

            registry.RegisterDevice(device, deviceId);

            ApplicationContext.ServiceRegistry.RegisterService(typeof(DeviceRegistry), registry);

            MqttMessageReceived message = new MqttMessageReceived("/Command", "01,DoMyMethod,YES");
            message.ProcessMessage();

            Assert.IsTrue(device.TestSuccess);
        }
    }
}
