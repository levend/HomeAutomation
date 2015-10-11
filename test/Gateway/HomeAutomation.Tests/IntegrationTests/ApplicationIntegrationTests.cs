using HomeAutomation.Application;
using HomeAutomation.Communication.Mqtt;
using HomeAutomation.Core;
using HomeAutomation.Tests.IntegrationTests.Factory;
using HomeAutomation.Util;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using System.Collections.Generic;

namespace HomeAutomation.Tests.IntegrationTests
{
    [TestClass]
    public class ApplicationIntegrationTests
    {
        MockXBeeSerialPort serialPort;
        MockMqttClient mqttClient;

        [TestInitialize]
        public void Setup()
        {
            MainApplication.Start("IntegrationTests/Config/MockConfig.conf");

            HomeAutomationSystem.ServiceRegistry.Runner.Stop(); // we will make sure to "step" the system, not run

            serialPort = (MockXBeeSerialPort)MockXBeeSerialPortFactory.Instance.Create(null);
            mqttClient = (MockMqttClient)MockMqttClientFactory.Instance.Create(null);
        }

        [TestMethod]
        public void TestDeviceIdentificationFlow()
        {
            // TODO: Ensure watchdog, statistics or any other time based service does not mess with out results.

            IDeviceNetwork dn = HomeAutomationSystem.DeviceNetworkRegistry.GetNetworkByName("xbee");

            // STEP1: Device sends IO Data sample. Result: device is not known, so a message should be sent back to it asking for identification.
            serialPort.EnqueueFrame(HexConverter.BytesFromSpacedString("7E 00 12 92 12 34 56 78 12 34 56 78 11 11 00 01 00 00 00 02 E5 3B")); // analog reading: 724 mV
            HomeAutomationSystem.ServiceRegistry.Runner.StepOneLoop();

            // check remote at command request
            {
                // check the result frames written
                List<byte[]> writtenFrames = serialPort.FlushWritternFrames();

                Assert.AreEqual(1, writtenFrames.Count);
                RemoteATCommand remoteCommand = FrameSerializer.Deserialize(writtenFrames[0]) as RemoteATCommand;

                Assert.IsTrue(XBeeFrameUtil.IsSameATCommand(remoteCommand.ATCommand, ATCommands.DD)); // Is this DD request ?
                Assert.AreEqual<string>("1234567812345678", remoteCommand.Address.ToHexString()); // Is the address correct ?

                // check if the device is staged
                Assert.IsTrue(HomeAutomationSystem.DeviceRegistry.IsStagingDevice(dn, "1234567812345678".BytesFromString()));
            }

            // STEP2: Device identifies itself
            serialPort.EnqueueFrame(HexConverter.BytesFromSpacedString("7E 00 13 97 01 12 34 56 78 12 34 56 78 11 11 44 44 00 00 03 99 88 71")); // identification 0x9988
            HomeAutomationSystem.ServiceRegistry.Runner.StepOneLoop();

            {
                // the device should not be staged anymore
                Assert.IsFalse(HomeAutomationSystem.DeviceRegistry.IsStagingDevice(dn, "1234567812345678".BytesFromString()));
            }

            // STEP3: Device sends temperature reading
            serialPort.EnqueueFrame(HexConverter.BytesFromSpacedString("7E 00 12 92 12 34 56 78 12 34 56 78 11 11 00 01 00 00 00 02 E5 3B")); // analog reading: 724 mV
            HomeAutomationSystem.ServiceRegistry.Runner.StepOneLoop();

            {
                List<MqttMessage> sentMessages = mqttClient.FlushSentMessages();

                // There should be one message:
                // Message: "1234567812345678,MCP9700,36.8", Topic: /MosziNet_HA/Status
                Assert.AreEqual<int>(1, sentMessages.Count);
                Assert.AreEqual<string>("1234567812345678,MCP9700,36.8", sentMessages[0].Message);
                Assert.AreEqual<string>("/MosziNet_HA/Status", sentMessages[0].TopicName);
            }
        }

    }
}
