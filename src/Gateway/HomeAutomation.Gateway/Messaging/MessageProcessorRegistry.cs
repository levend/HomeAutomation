using System;
using System.Collections;
using System.Collections.Generic;

namespace MosziNet.HomeAutomation.Messaging
{
    public class MessageProcessorRegistry
    {
        private Dictionary<Type, IMessageProcessor> messageProcessors = new Dictionary<Type, IMessageProcessor>();

        public void RegisterMessageProcessor(Type messageType, IMessageProcessor processor)
        {
            messageProcessors.Add(messageType, processor);
        }

        public IMessageProcessor GetMessageProcessorByMessage(IMessage message)
        {
            return messageProcessors.ContainsKey(message.GetType()) ? messageProcessors[message.GetType()] : null;
        }
    }
}
