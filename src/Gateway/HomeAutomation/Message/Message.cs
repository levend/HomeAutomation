using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation
{
    /// <summary>
    /// Describes a message that is posted by devices, sensors, or other entities in the system.
    /// </summary>
    public class Message
    {
        public Message(string messageType, string message)
        {
            this.MessageType = messageType;
            this.MessageContent = message;
        }

        public string MessageType { get; private set; }

        public string MessageContent { get; private set; }
    }
}
