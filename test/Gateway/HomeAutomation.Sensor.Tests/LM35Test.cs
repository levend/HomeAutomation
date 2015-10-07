using System;
using MosziNet.HomeAutomation.Sensor.Temperature;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace HomeAutomation.Sensor.Tests
{
    [TestClass]
    public class LM35Test
    {
        [TestMethod]
        public void TestTemperatureFromVoltage()
        {
            Assert.IsTrue(LM35.TemperatureFromVoltage(350) == 35);
        }
    }
}
