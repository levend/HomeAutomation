using System;
using Microsoft.SPOT;
using System.Collections;
using MosziNet.HomeAutomation.Logging;

namespace MosziNet.HomeAutomation.Messaging
{
    public class StandardMessageBusRunner : IMessageBusRunner
    {
        private MessageProcessorRegistry messageProcessorRegistry;
        private IMessageBus messageBus;

        public StandardMessageBusRunner(IMessageBus bus, MessageProcessorRegistry processorRegistry)
        {
            messageBus = bus;
            messageProcessorRegistry = processorRegistry;
        }

        public bool ProcessMessage(IMessage message)
        {
            IMessageProcessor processor = messageProcessorRegistry.GetMessageProcessorByMessage(message);

            if (processor != null)
            {
                // process the message
                processor.ProcessMessage(message);
            }
            else
            {
                Log.Debug("Dropping message of type: " + message.GetType().FullName);
            }

            return true;
        }
    }
}
