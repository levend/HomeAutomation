using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.Logging
{
    public interface ILogFormatter
    {
        string Format(string message, LogLevel logLevel, DateTime logTime);
    }
}
