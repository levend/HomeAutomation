using System;
using HomeAutomation.Sensor.Temperature;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

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
