using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using MosziNet.HomeAutomation.XBee.Frame;

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

            byte[] testBuffer = new byte[buffer.Length];

            FrameSerializer.Serialize(frame, testBuffer);

            Assert.AreEqual(buffer.Length, testBuffer.Length);

            for (int i = 0; i < buffer.Length; i++)
            {
                Assert.AreEqual<byte>(buffer[i], testBuffer[i]);
            }
        }
    }
}
