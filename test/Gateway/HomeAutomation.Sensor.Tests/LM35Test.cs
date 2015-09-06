using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MosziNet.HomeAutomation.Sensor.Temperature;

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
