using HomeAutomation.Application.Configuration;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.Devices.SerialCommunication;

namespace HomeAutomation.Application.Tests
{
    [TestClass]
    public class ConfigurationDeserializationTests
    {
        [TestMethod]
        public void TestConfigurationDeserialization()
        {
            var config = new ConfigurationManager().LoadFile("HomeAutomation.Applications.Tests/Config/HomeAutomationTest.conf");

            Assert.IsNotNull(config);

            Assert.IsNotNull(config.XBee);
            Assert.AreEqual((uint)9600, config.XBee.BaudRate);
            Assert.AreEqual(SerialParity.None, config.XBee.SerialParity);
            Assert.AreEqual(SerialStopBitCount.One, config.XBee.SerialStopBitCount);
            Assert.AreEqual((ushort)8, config.XBee.DataBits);

            Assert.IsNotNull(config.XBeeSerialPortFactory);
            Assert.IsNotNull(config.MqttClientFactory);
        }
    }
}
