using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation
{
    /// <summary>
    /// Describes a message that is posted by devices, sensors, or other entities in the system.
    /// </summary>
    public class Message
    {
        public string MessageType { get; protected set; }
    }
}
