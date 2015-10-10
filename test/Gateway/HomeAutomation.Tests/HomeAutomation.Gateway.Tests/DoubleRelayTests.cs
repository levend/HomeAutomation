using HomeAutomation.Core;
using HomeAutomation.DeviceNetwork.XBee;
using HomeAutomation.DeviceNetwork.XBee.Device;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.XBee.Frame;
using System;

namespace HomeAutomation.Tests
{
    [TestClass]
    public class DoubleRelayTests
    {
        public class MockSerialPort : IXBeeSerialPort
        {
            public byte[] GetNextAvailableFrame()
            {
                return null;
            }

            public void WriteFrame(byte[] frame)
            {
                
            }
        }

        [TestMethod]
        public void TestDoubleRelaySwitchStates()
        {
            XBeeDeviceNetwork dn = new XBeeDeviceNetwork(new MockSerialPort());

            DoubleRelay device = new DoubleRelay();
            device.DeviceNetwork = dn;
            device.DeviceID = new byte[] { 1 };

            device.SetRelayState(0, 1);
            Assert.IsTrue(device.DeviceState.ComponentStateList[0].Value == "1");

            device.SetRelayState(0, 0);
            Assert.IsTrue(device.DeviceState.ComponentStateList[0].Value == "0");

            device.SetRelayState(0, 2);
            Assert.IsTrue(device.DeviceState.ComponentStateList[0].Value == "1");

            device.SetRelayState(1, 0);
            Assert.IsTrue(device.DeviceState.ComponentStateList[1].Value == "0");

            device.SetRelayState(1, 1);
            Assert.IsTrue(device.DeviceState.ComponentStateList[1].Value == "1");

            device.SetRelayState(2, 0);
            Assert.IsTrue(device.DeviceState.ComponentStateList[1].Value == "0");

            device.SetRelayState(2, 1);
            Assert.IsTrue(device.DeviceState.ComponentStateList[1].Value == "1");

        }
    }
}
