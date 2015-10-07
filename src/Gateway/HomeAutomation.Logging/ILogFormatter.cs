using System;

namespace MosziNet.HomeAutomation.Logging
{
    public interface ILogFormatter
    {
        string Format(string message, LogLevel logLevel, DateTime logTime);
    }
}
