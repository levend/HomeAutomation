using HomeAutomation.Core;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MosziNet.XBee;
using MosziNet.XBee.Frame;
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
                MessageReceived?.Invoke(null, null);
            }

            public void SendFrame(IXBeeFrame frame)
            {
                
            }
        }

         public class MockDeviceNetwok : IDeviceNetwork
        {
            public string Name { get; set; }

            //public DeviceTypeDescription[] AvailableDeviceTypes
            //{
            //    get
            //    {
            //        return new DeviceTypeDescription[]
            //        {
            //            new DeviceTypeDescription(0x9999, typeof(MockTestDevice))
            //        };
            //    }
            //}

            public event EventHandler<DeviceState> DeviceStateReceived;

            public void SendCommand(DeviceCommand command)
            {
                HomeAutomationSystem.DeviceRegistry.GetDeviceById(this, command.DeviceID).ExecuteCommand(command);
            }

            public void GenerateDeviceState(DeviceState state)
            {
                DeviceStateReceived?.Invoke(this, state);
            }

            public void UpdateDiagnostics()
            {
                
            }
        }

        [TestMethod]
        public void TestSimpleCommandExecution()
        {
            byte[] deviceId = new byte[] { 0x01 };

            MockTestDevice device = new MockTestDevice();
            device.DeviceID = deviceId;
            device.NetworkAddress = new byte[] { 1 };

            MockDeviceNetwok mdn = new MockDeviceNetwok();

            HomeAutomationSystem.DeviceNetworkRegistry.RegisterDeviceNetwork(mdn, "mock");
            HomeAutomationSystem.DeviceRegistry.RegisterDevice(mdn, device, deviceId);

            DeviceCommand dc = new DeviceCommand()
            {
                DeviceID = deviceId,
                DeviceNetworkName = "mock",
                Name = "DoMyMethod",
                Parameters = new string[] { "YES" }
            };

            mdn.SendCommand(dc);

            Assert.IsTrue(device.TestSuccess);
        }
    }
}
