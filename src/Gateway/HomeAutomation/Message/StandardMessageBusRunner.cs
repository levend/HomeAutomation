using System;
using Microsoft.SPOT;
using System.Collections;

namespace MosziNet.HomeAutomation
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

        public bool ProcessMessage(Message message)
        {
            IMessageProcessor processor = messageProcessorRegistry.GetMessageProcessorByMessage(message);

            if (processor != null)
            {
                // process the message
                processor.ProcessMessage(message);
            }
            else
            {
                Debug.Print("Dropping message of type: " + message.GetType().FullName);
            }

            return true;
        }
    }
}
