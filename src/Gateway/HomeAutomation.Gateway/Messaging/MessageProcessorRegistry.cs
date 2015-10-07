using System;
using System.Collections;

namespace MosziNet.HomeAutomation.Messaging
{
    public class MessageProcessorRegistry
    {
        private Hashtable messageProcessors = new Hashtable();

        public void RegisterMessageProcessor(Type messageType, IMessageProcessor processor)
        {
            messageProcessors.Add(messageType, processor);
        }

        public IMessageProcessor GetMessageProcessorByMessage(IMessage message)
        {
            return messageProcessors.Contains(message.GetType()) ? (IMessageProcessor)messageProcessors[message.GetType()] : null;
        }
    }
}
