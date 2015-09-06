using System;
using Microsoft.SPOT;
using System.Collections;

namespace MosziNet.HomeAutomation
{
    public class MessageProcessorRegistry
    {
        private Hashtable messageProcessors = new Hashtable();

        public void RegisterMessageProcessor(string messageProcessorType, IMessageProcessor processor)
        {
            lock (messageProcessors)
            {
                messageProcessors.Add(messageProcessorType, processor);
            }
        }

        public IMessageProcessor GetMessageProcessorByMessage(Message message)
        {
            lock(messageProcessors)
            {
                return messageProcessors.Contains(message.MessageType) ? (IMessageProcessor)messageProcessors[message.MessageType] : null;
            }
        }
    }
}
