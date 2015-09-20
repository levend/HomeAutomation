using System;
using Microsoft.SPOT;
using System.Collections;

namespace MosziNet.HomeAutomation.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public static class Log
    {
        private static ArrayList logWriters = new ArrayList();

        public static void AddLogWriter(ILogWriter logWriter)
        {
            logWriters.Add(logWriter);
        }

        public static void Debug(string message)
        {
            foreach(ILogWriter writer in logWriters)
            {
                writer.Log(message, LogLevel.Debug);
            }
        }
    }
}
