using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.Logging
{
    public enum LogLevel
    {
        Debug,
        Error,
        Information
    }

    public interface ILogWriter
    {
        void Log(string message, LogLevel logLevel, ILogFormatter logFormatter);
    }
}
