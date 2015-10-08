using System;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.XBee;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace HomeAutomation.XBee.Tests
{
    [TestClass]
    public class ZigBeeFrameSerializationTests
    {
        [TestMethod]
        public void TestIODataSampleDeserialization()
        {
            byte[] buffer = new byte[] { 0x7E, 0x00, 0x13, 0x97, 0x01, 0x00, 0x13, 0xA2, 0x00, 0x40, 0xE4, 0x38, 0xE3, 0x55, 0x42, 0x44, 0x44, 0x00, 0x00, 0x03, 0x99, 0x88, 0x30 };

            RemoteCommandResponse frame = (RemoteCommandResponse)FrameSerializer.Deserialize(buffer);

            frame.Address.ToString();
        }

        [TestMethod]
        public void TestIODataSampleDeserializationSerialization()
        {
            byte[] buffer = new byte[] { 0x7E, 0x00, 0x13, 0x97, 0x01, 0x00, 0x13, 0xA2, 0x00, 0x40, 0xE4, 0x38, 0xE3, 0x55, 0x42, 0x44, 0x44, 0x00, 0x00, 0x03, 0x99, 0x88, 0x30 };

            RemoteCommandResponse frame = (RemoteCommandResponse)FrameSerializer.Deserialize(buffer);

            byte[] testBuffer = FrameSerializer.Serialize(frame);

            Assert.AreEqual(buffer.Length, testBuffer.Length);

            for (int i = 0; i < buffer.Length; i++)
            {
                Assert.AreEqual<byte>(buffer[i], testBuffer[i]);
            }
        }

        [TestMethod]
        public void TestAnalogReadings()
        {
            byte[] buffer = new byte[] { 
                0x7E, 0x00, 0x12, 0x92, 0x00, 0x13, 0xA2, 0x00, 0x40, 0xE4, 
                0x38, 0xE3, 0x82, 0x8F, 0x01, 0x01, 0x00, 0x00, 0x01, 0x02, 
                0x6C, 0xF7 };

            MockSerialPort msp = new MockSerialPort(buffer);
            XBeeService xbeeService = new XBeeService(msp);

            IODataSample f = null;
            xbeeService.MessageReceived += (sender, frame) =>
            {
                f = frame as IODataSample;
            };

            xbeeService.ProcessXBeeMessages();

            Assert.IsNotNull(f);
            Assert.AreEqual(0x02, f.Samples[0]);
            Assert.AreEqual(0x6c, f.Samples[1]);
        }

        [TestMethod]
        public void TestHWAddressReading()
        {
            byte[] buffer = new byte[] { 0x7E, 0x00, 0x12, 0x92, 0x00, 0x13, 0xA2, 0x00, 0x40, 0xE4, 0x38, 0xE3, 0x82, 0x8F, 0x01, 0x01, 0x00, 0x00, 0x01, 0x02, 0x6C, 0xF7 };

            MockSerialPort msp = new MockSerialPort(buffer);

            XBeeService xbeeService = new XBeeService(msp);

            IODataSample f = null;
            xbeeService.MessageReceived += (sender, frame) =>
            {
                f = frame as IODataSample;
            };
            xbeeService.ProcessXBeeMessages();

            Assert.IsNotNull(f);
            byte[] expectedAddress = new byte[] { 0x00, 0x13, 0xA2, 0x00, 0x40, 0xE4, 0x38, 0xE3 };
            for (int i = 0; i < f.Address.Length; i++)
            {
                Assert.AreEqual<byte>(f.Address[i], expectedAddress[i]);
            }
        }

    }
}
