using HomeAutomation.Application;
using HomeAutomation.Communication.Mqtt;
using HomeAutomation.Core;
using HomeAutomation.Tests.IntegrationTests.Factory;
using HomeAutomation.Util;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MosziNet.XBee;
using MosziNet.XBee.Frame.ZigBee;
using System.Collections.Generic;

namespace HomeAutomation.Tests.IntegrationTests
{
    [TestClass]
    public class ApplicationIntegrationTests
    {
        MockXBeeSerialPort serialPort;
        MockMqttClient mqttClient;
        IDeviceNetwork xbeeNetwork;

        [TestInitialize]
        public void Setup()
        {
            MainApplication.Initialize("IntegrationTests/Config/MockConfig.json");

            serialPort = (MockXBeeSerialPort)MockXBeeDeviceNetworkFactory.SerialPort;
            mqttClient = MockMqttControllerFactory.Client;
            xbeeNetwork = HomeAutomationSystem.DeviceNetworkRegistry.GetNetworkByName("xbee");
        }

        [TestMethod]
        public void TestIntegrationFlows()
        {
            // TODO: Ensure watchdog, statistics or any other time based service does not mess with out results.

            TestDeviceIdentificationFlow();
            TestDeviceCommandFlow();
        }

        private void TestDeviceIdentificationFlow()
        {
            // STEP1: Device sends IO Data sample. Result: device is not known, so a message should be sent back to it asking for identification.
            serialPort.EnqueueFrame(HexConverter.BytesFromSpacedString("7E 00 12 92 12 34 56 78 12 34 56 78 11 11 00 01 00 00 00 02 E5 3B")); // analog reading: 724 mV
            HomeAutomationSystem.ScheduledTasks.Runner.Step();

            // check remote at command request
            {
                // check the result frames written
                List<byte[]> writtenFrames = serialPort.FlushWritternFrames();

                Assert.AreEqual(1, writtenFrames.Count);
                RemoteATCommand remoteCommand = FrameSerializer.Deserialize(writtenFrames[0]) as RemoteATCommand;

                Assert.IsTrue(XBeeFrameUtil.IsSameATCommand(remoteCommand.ATCommand, ATCommands.DD)); // Is this DD request ?
                Assert.AreEqual<string>("1234567812345678", remoteCommand.Address.ToHexString()); // Is the address correct ?

                // check if the device is staged
                Assert.IsTrue(HomeAutomationSystem.DeviceRegistry.IsStagingDevice(xbeeNetwork, "1234567812345678".BytesFromString()));
            }

            // STEP2: Device identifies itself
            serialPort.EnqueueFrame(HexConverter.BytesFromSpacedString("7E 00 13 97 01 12 34 56 78 12 34 56 78 11 11 44 44 00 00 03 99 88 71")); // identification 0x9988
            HomeAutomationSystem.ScheduledTasks.Runner.Step();

            {
                // the device should not be staged anymore
                Assert.IsFalse(HomeAutomationSystem.DeviceRegistry.IsStagingDevice(xbeeNetwork, "1234567812345678".BytesFromString()));
            }
            mqttClient.FlushSentMessages();

            // STEP3: Device sends temperature reading
            serialPort.EnqueueFrame(HexConverter.BytesFromSpacedString("7E 00 12 92 12 34 56 78 12 34 56 78 11 11 00 01 00 00 00 02 E5 3B")); // analog reading: 724 mV
            HomeAutomationSystem.ScheduledTasks.Runner.Step();

            {
                List<MqttMessage> sentMessages = mqttClient.FlushSentMessages();

                // There should be one message:
                // Message: "1234567812345678,MCP9700,36.8", Topic: /MosziNet_HA/Status
                Assert.AreEqual<int>(1, sentMessages.Count);
                Assert.AreEqual<string>("xbee,1234567812345678,MCP9700,36.8", sentMessages[0].Message);
                Assert.AreEqual<string>("/MosziNet_HA/Status", sentMessages[0].TopicName);
            }
        }

        public void TestDeviceCommandFlow()
        {
            // STEP 1: send frame from Double Relay, id itself.
            serialPort.EnqueueFrame(HexConverter.BytesFromSpacedString("7E 00 12 92 12 34 56 78 11 22 33 44 11 22 00 01 00 03 00 00 03 75"));
            HomeAutomationSystem.ScheduledTasks.Runner.Step();

            serialPort.EnqueueFrame(HexConverter.BytesFromSpacedString("7E 00 13 97 01 12 34 56 78 11 22 33 44 11 22 44 44 00 00 03 99 84 CE"));
            HomeAutomationSystem.ScheduledTasks.Runner.Step();

            serialPort.EnqueueFrame(HexConverter.BytesFromSpacedString("7E 00 12 92 12 34 56 78 11 22 33 44 11 22 00 01 00 03 00 00 03 75"));
            HomeAutomationSystem.ScheduledTasks.Runner.Step();

            // STEP 2: generate a command on the command topic to switch relay at pin D0 to ON
            mqttClient.FlushSentMessages();
            serialPort.FlushWritternFrames();

            // generate a switch command from the controller 
            mqttClient.GenerateMessageOnTopic("/MosziNet_HA/Command", "xbee,1234567811223344,SetRelayState,0,1");
            HomeAutomationSystem.ScheduledTasks.Runner.Step();

            // check remote at command request
            {
                // check the result frames written
                List<byte[]> writtenFrames = serialPort.FlushWritternFrames();

                Assert.AreEqual(1, writtenFrames.Count);
                RemoteATCommand remoteCommand = FrameSerializer.Deserialize(writtenFrames[0]) as RemoteATCommand;

                Assert.IsTrue(XBeeFrameUtil.IsSameATCommand(remoteCommand.ATCommand, ATCommands.D0)); // Is this D0 write request ?
                Assert.AreEqual<string>("1234567811223344", remoteCommand.Address.ToHexString()); // Is the address correct ?
                Assert.AreEqual(0x04, remoteCommand.Parameters[0]);
            }
        }
    }
}
