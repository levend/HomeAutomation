using System;
using MosziNet.HomeAutomation.Util;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace HomeAutomation.Util.Tests
{
    [TestClass]
    public class HexConverterTests
    {
        [TestMethod]
        public void TestHexConvert()
        {
            string result = HexConverter.ToHexString(new byte[] { 0xAA, 0x12, 0xBB, 0xFF });

            Assert.AreEqual("AA12BBFF", result);
        }

        [TestMethod]
        public void TextConvert2Way()
        {
            byte[] bytes = new byte[] { 0x00, 0x12, 0x34, 0x56, 0x78, 0x89, 0xAB, 0xCD, 0xEF };

            string intermediateConversion = HexConverter.ToHexString(bytes);
            byte[] result = HexConverter.BytesFromString(intermediateConversion);

            // first test the length
            Assert.AreEqual(bytes.Length, result.Length);

            // the test the bytes returned
            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual<byte>(bytes[i], result[i]);
            }
        }
    }
}
