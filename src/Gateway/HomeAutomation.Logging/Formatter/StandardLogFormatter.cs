using System;

namespace MosziNet.HomeAutomation.Logging.Formatter
{
    public class StandardLogFormatter : ILogFormatter
    {
        public string Format(string message, LogLevel logLevel, DateTime logTime)
        {
            return "[" + logTime.ToString("yyyy-MM-dd HH:mm:ss") + "] " + LogLevel2String(logLevel) + " - " + message;
        }

        private string LogLevel2String(LogLevel logLevel)
        {
            switch(logLevel)
            {
                case LogLevel.Debug:
                    return "Debug";
                case LogLevel.Error:
                    return "Error";
                case LogLevel.Information:
                    return "Information";
                default:
                    return "UNKNOWN_LOGLEVEL";
            }
        }
    }
}
