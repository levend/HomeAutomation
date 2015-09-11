using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.XBee.Frame;

namespace HomeAutomation.XBee.Tests
{
    [TestClass]
    public class XBeeFrameDeserializationTests
    {
        [TestMethod]
        public void TestAnalogReadings()
        {
            byte[] buffer = new byte[] { 0x7E, 0x00, 0x12, 0x92, 0x00, 0x13, 0xA2, 0x00, 0x40, 0xE4, 0x38, 0xE3, 0x82, 0x8F, 0x01, 0x01, 0x00, 0x00, 0x01, 0x02, 0x6C, 0xF7 };

            XBeeSerialPortReader pr = new XBeeSerialPortReader();
            MockSerialPort msp = new MockSerialPort(buffer);

            IODataSampleFrame f = (IODataSampleFrame) pr.FrameFromSerialPort(msp);
            Assert.IsTrue(f.AnalogReadings[0] >  726 && f.AnalogReadings[0] < 727);
        }

        [TestMethod]
        public void TestHWAddressReading()
        {
            byte[] buffer = new byte[] { 0x7E, 0x00, 0x12, 0x92, 0x00, 0x13, 0xA2, 0x00, 0x40, 0xE4, 0x38, 0xE3, 0x82, 0x8F, 0x01, 0x01, 0x00, 0x00, 0x01, 0x02, 0x6C, 0xF7 };

            XBeeSerialPortReader pr = new XBeeSerialPortReader();
            MockSerialPort msp = new MockSerialPort(buffer);

            IODataSampleFrame f = (IODataSampleFrame)pr.FrameFromSerialPort(msp);

            byte[] expectedAddress = new byte[] { 0x00, 0x13, 0xA2, 0x00, 0x40, 0xE4, 0x38, 0xE3 };
            for (int i = 0; i < f.Address.Length; i++ )
            {
                Assert.AreEqual<byte>(f.Address[i], expectedAddress[i]);
            }
        }
    }
}
