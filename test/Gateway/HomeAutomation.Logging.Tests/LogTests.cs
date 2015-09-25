using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Logging.Formatter;

namespace HomeAutomation.Logging.Tests
{
    [TestClass]
    public class LogTests
    {
        public class TestLogWriter : ILogWriter
        {
            public string LastMessage;
            public LogLevel LastLogLevel;
            public ILogFormatter LastLogFormatter;

            public void Log(string message, LogLevel logLevel, ILogFormatter logFormatter)
            {
                this.LastMessage = message;
                this.LastLogLevel = logLevel;
                this.LastLogFormatter = logFormatter;
            }
        }

        public class TestLogWriterWithFormatter : ILogWriter
        {
            public string LastMessage;
            public LogLevel LastLogLevel;
            public ILogFormatter LastLogFormatter;

            public DateTime LogTime;

            public void Log(string message, LogLevel logLevel, ILogFormatter logFormatter)
            {
                this.LastMessage = logFormatter.Format(message, logLevel, LogTime);
                this.LastLogLevel = logLevel;
                this.LastLogFormatter = logFormatter;
            }
        }

        [TestMethod]
        public void TestLogWriters()
        {
            TestLogWriter tlw = new TestLogWriter();

            Log.AddLogWriter(tlw, null);

            Log.Debug("Test1");

            Assert.AreEqual("Test1", tlw.LastMessage);
            Assert.AreEqual(LogLevel.Debug, tlw.LastLogLevel);
            Assert.IsNull(tlw.LastLogFormatter);

            Log.Information("Test2");
            Assert.AreEqual("Test2", tlw.LastMessage);
            Assert.AreEqual(LogLevel.Information, tlw.LastLogLevel);

            Log.Error("Test3");
            Assert.AreEqual("Test3", tlw.LastMessage);
            Assert.AreEqual(LogLevel.Error, tlw.LastLogLevel);
        }

        [TestMethod]
        public void TestLogFormatter()
        {
            TestLogWriterWithFormatter tlw = new TestLogWriterWithFormatter();
            StandardLogFormatter lf = new StandardLogFormatter();

            Log.AddLogWriter(tlw, lf);

            tlw.LogTime = new DateTime(2000, 1, 2, 13, 14, 15);
            Log.Debug("Test1");

            Assert.AreEqual("[2000-01-02 13:14:15] Debug - Test1", tlw.LastMessage);
            Assert.AreEqual(LogLevel.Debug, tlw.LastLogLevel);
            Assert.AreEqual(lf, tlw.LastLogFormatter);

        }
    }
}
