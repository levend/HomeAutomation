using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MosziNet.HomeAutomation.XBee;

namespace HomeAutomation.XBee.Tests
{
    [TestClass]
    public class XBeeFrameDeserializationTests
    {
        [TestMethod]
        public void TestAnalogReadings()
        {
            byte[] buffer = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 0};

            XBeeFrame f = XBeeFrameSerialization.Deserialize(buffer, 18);

            // Assert.IsTrue(true);
            // TODO
        }
    }
}
