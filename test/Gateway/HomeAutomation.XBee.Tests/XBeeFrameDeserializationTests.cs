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
            byte[] buffer = new byte[] { 0x7E, 0x00, 0x17, 0x94, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x68};

            XBeeSerialPortReader pr = new XBeeSerialPortReader();
            MockSerialPort msp = new MockSerialPort(buffer);

            IODataSampleFrame f = (IODataSampleFrame) pr.FrameFromSerialPort(msp);
            Assert.IsTrue(f.AnalogReadings[0] > 302 && f.AnalogReadings[0] < 303);
        }

        [TestMethod]
        public void TestHWAddressReading()
        {
            byte[] buffer = new byte[] { 0x7E, 0x00, 0x17, 0x94, 0x12, 0x34, 0x56, 0x78, 0x77, 0x88, 0x11, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00, 0x68 };

            XBeeSerialPortReader pr = new XBeeSerialPortReader();
            MockSerialPort msp = new MockSerialPort(buffer);

            IODataSampleFrame f =(IODataSampleFrame) pr.FrameFromSerialPort(msp);
            
            // Todo
            //Assert.IsTrue(f.Address == "1234567877881100");
        }
    }
}
