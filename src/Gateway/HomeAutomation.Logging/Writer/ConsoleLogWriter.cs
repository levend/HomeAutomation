using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.Logging.Writer
{
    public class ConsoleLogWriter : ILogWriter
    {
        public void Log(string message, LogLevel logLevel, ILogFormatter logFormatter)
        {
            Debug.Print(logFormatter.Format(message, logLevel));
        }
    }
}
