using System;
using Microsoft.SPOT;
using System.Threading;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.Messaging
{
    public class ThreadedMessageBusRunner : IMessageBusRunner
    {
        private bool shouldMessageBusRunnerContinue;
        private MessageProcessorRegistry messageProcessorRegistry;
        private IMessageBus messageBus;

        public ThreadedMessageBusRunner(IMessageBus bus, MessageProcessorRegistry processorRegistry)
        {
            messageBus = bus;
            messageProcessorRegistry = processorRegistry;

            StartProcessingMessages();
        }

        public bool ProcessMessage(IMessage message)
        {
            // do nothing here as we are processing messages on a separate thread
            return false;
        }

        /// <summary>
        /// Stops processing bus messages.
        /// </summary>
        public void StopProcessingMessages()
        {
            shouldMessageBusRunnerContinue = false;
        }

        private void StartProcessingMessages()
        {
            shouldMessageBusRunnerContinue = true;

            new Thread(this.MessageProcessorLoop).Start();
        }

        private void MessageProcessorLoop()
        {
            while (shouldMessageBusRunnerContinue)
            {
                try
                {
                    ProcessMessagesFromQueue();
                }
                catch (Exception ex)
                {
                    Log.Error("[MessageProcessorLoop Exception] " + ExceptionFormatter.Format(ex));
                }
            }
        }

        private void ProcessMessagesFromQueue()
        {
            IMessage message;

            // while there are messages that can be dequeued from the message bus, dequeue and process them.
            while ((message = messageBus.DequeueMessage()) != null)
            {
                IProcessableMessage processableMessage = message as IProcessableMessage;
                if (processableMessage != null)
                {
                    processableMessage.ProcessMessage();
                }
                else
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
                }
            }

            Thread.Sleep(100);
        }

    }
}
