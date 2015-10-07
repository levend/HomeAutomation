using System;

namespace MosziNet.HomeAutomation.Messaging
{
    public interface IMessageBusRunner
    {
        /// <summary>
        /// Returns true in case the message was processed, or false if it was not and the message needs to be enqueued for later processing
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        bool ProcessMessage(IMessage message);
    }
}
