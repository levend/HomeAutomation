using System;
using HomeAutomation.DeviceNetwork.XBee;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MosziNet.Devices.XBee;
using MosziNet.XBee;

namespace HomeAutomation.Tests
{
    [TestClass]
    public class DoubleRelayTests
    {
        public class MockSerialPort : IXBeeSerialPort
        {
            public bool SerialPortConnected
            {
                get
                {
                    return true;
                }
            }

            public event EventHandler<EventArgs> SerialPortConnectionChanged;

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
