using HomeAutomation.Application;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace HomeAutomation.Tests.IntegrationTests
{
    [TestClass]
    public class ApplicationInitializationTest
    {
        [TestMethod]
        public void Setup()
        {
            MainApplication.Start("IntegrationTests/Config/MockConfig.conf");
        }
    }
}
