using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.Logging.Formatter
{
    public class StandardLogFormatter : ILogFormatter
    {
        public string Format(string message, LogLevel logLevel)
        {
            return "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + logLevel.ToString() + " - " + message;
        }
    }
}
