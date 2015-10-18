using HomeAutomation.Application.Configuration;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace HomeAutomation.Application.Tests
{
    [TestClass]
    public class ConfigurationDeserializationTests
    {
        [TestMethod]
        public void TestConfigurationDeserialization()
        {
            var config = new ConfigurationManager().LoadFile<HomeAutomationConfiguration>("HomeAutomation.Applications.Tests/Config/HomeAutomationConfiguration.json");

            Assert.IsNotNull(config);

            Assert.AreEqual(1, config.DeviceNetworks.Count);
            Assert.AreEqual(4, config.DeviceTypes.Count);
        }
    }
}
