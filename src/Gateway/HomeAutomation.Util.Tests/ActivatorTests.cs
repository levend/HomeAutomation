using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace HomeAutomation.Util.Tests
{
    [TestClass]
    public class ActivatorTests
    {
        [TestMethod]
        public void TestCreateInstance()
        {
            object instance = MosziNet.HomeAutomation.Util.Activator.CreateInstance(typeof(ArrayList));

            Assert.IsInstanceOfType(instance, typeof(ArrayList));
        }
    }
}
