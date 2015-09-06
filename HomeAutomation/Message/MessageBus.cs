using System;
using Microsoft.SPOT;
using System.Collections;
using System.Threading;

namespace MosziNet.HomeAutomation
{
    /// <summary>
    /// Provides a FIFO queue for message processing.
    /// </summary>
    public class MessageBus : MosziNet.HomeAutomation.IMessageBus
    {
        private ArrayList messageList = new ArrayList();

        public IMessageBusRunner MessageBusRunner { get; set; }

        /// <summary>
        /// Adds a message to the end of the message queue.
        /// </summary>
        /// <param name="message"></param>
        public void PostMessage(Message message)
        {
            IMessageBusRunner runner = this.MessageBusRunner;
            if (runner != null)
            {
                // only queue the message if the runner processed it immediately 
                if (!runner.ProcessMessage(message))
                {
                    lock (messageList)
                    {
                        messageList.Add(message);
                    }
                }
            }
        }

        /// <summary>
        /// Returns the first message from the queue, or null if it's empty
        /// </summary>
        /// <returns></returns>
        public Message DequeueMessage()
        {
            Message firstMessage = null;

            lock (messageList)
            {
                if (messageList.Count > 0)
                {
                    firstMessage = (Message)messageList[0];
                    messageList.RemoveAt(0);
                }
            }

            return firstMessage;
        }
    }
}
