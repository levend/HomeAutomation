using System.Collections.Generic;

namespace MosziNet.HomeAutomation.Messaging
{
    /// <summary>
    /// Provides a FIFO queue for message processing.
    /// </summary>
    public class MessageBus : IMessageBus
    {
        private List<IMessage> messageList = new List<IMessage>();

        public IMessageBusRunner MessageBusRunner { get; set; }

        /// <summary>
        /// Adds a message to the end of the message queue.
        /// </summary>
        /// <param name="message"></param>
        public void PostMessage(IMessage message)
        {
            IMessageBusRunner runner = this.MessageBusRunner;
            if (runner != null)
            {
                // only queue the message if the runner processed it immediately 
                if (!runner.ProcessMessage(message))
                {
                    messageList.Add(message);
                }
            }
        }

        /// <summary>
        /// Returns the first message from the queue, or null if it's empty
        /// </summary>
        /// <returns></returns>
        public IMessage DequeueMessage()
        {
            IMessage firstMessage = null;

            if (messageList.Count > 0)
            {
                firstMessage = messageList[0];
                messageList.RemoveAt(0);
            }

            return firstMessage;
        }
    }
}
