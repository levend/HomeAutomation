using System;
using Microsoft.SPOT;
using System.Collections;

namespace MosziNet.HomeAutomation
{
    public class MessageProcessorRegistry
    {
        private Hashtable messageProcessors = new Hashtable();

        public void RegisterMessageProcessor(Type messageType, IMessageProcessor processor)
        {
            lock (messageProcessors)
            {
                messageProcessors.Add(messageType, processor);
            }
        }

        public IMessageProcessor GetMessageProcessorByMessage(Message message)
        {
            lock(messageProcessors)
            {
                return messageProcessors.Contains(message.GetType()) ? (IMessageProcessor)messageProcessors[message.GetType()] : null;
            }
        }
    }
}
