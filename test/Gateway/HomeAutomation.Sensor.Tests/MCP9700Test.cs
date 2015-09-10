using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MosziNet.HomeAutomation.Sensor.Temperature;

namespace HomeAutomation.Sensor.Tests
{
    [TestClass]
    public class MCP9700Test
    {
        [TestMethod]
        public void TestTemperatureFromMillivoltsConversion()
        {
            Assert.AreEqual(MCP9700.TemperatureFromVoltage(750), 25);
        }
    }
}
