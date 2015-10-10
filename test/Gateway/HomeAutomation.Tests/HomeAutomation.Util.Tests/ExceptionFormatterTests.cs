using System;
using HomeAutomation.Util;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace HomeAutomation.Util.Tests
{
    [TestClass]
    public class ExceptionFormatterTests
    {
        [TestMethod]
        public void TestFormatForEmptyException()
        {
            Exception ex = new Exception();

            string s = ex.FormatToLog();

            Assert.IsNotNull(s);
        }
    

        [TestMethod]
        public void TestFormatForExceptionWithMessage()
        {
            Exception ex = new Exception("moszi");

            string s = ex.FormatToLog();

            Assert.IsTrue(s.Contains("moszi"));
        }

        [TestMethod]
        public void TestFormatForNullException()
        {
            Exception ex = null;
            
            Assert.IsNotNull(ex.FormatToLog());
        }
    }

}
