using System;
using MosziNet.HomeAutomation.Util;
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

            string s = ExceptionFormatter.Format(ex);

            Assert.IsNotNull(s);
        }
    

        [TestMethod]
        public void TestFormatForExceptionWithMessage()
        {
            Exception ex = new Exception("moszi");

            string s = ExceptionFormatter.Format(ex);

            Assert.IsTrue(s.Contains("moszi"));
        }

        [TestMethod]
        public void TestFormatForNullException()
        {
            Exception ex = null;
            
            Assert.IsNotNull(ExceptionFormatter.Format(ex));
        }
    }

}
