using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation
{
    /// <summary>
    /// Describes a message that is posted by devices, sensors, or other entities in the system.
    /// </summary>
    public class Message
    {
        public string MessageType { get; private set; }

        public string MessageContent { get; private set; }

        public static Message Create(string messageType, string message)
        {
            Message m = new Message();

            m.MessageType = messageType;
            m.MessageContent = message;

            return m;

        }
    }
}
