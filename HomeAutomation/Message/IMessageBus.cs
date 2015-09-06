using System;

namespace MosziNet.HomeAutomation
{
    // Declare the delegate (if using non-generic pattern). 
    public delegate void MessageArrivedDelegate(Message message);

    public interface IMessageBus
    {
        IMessageBusRunner MessageBusRunner { get; set; }

        void PostMessage(Message message);
        Message DequeueMessage();
    }
}
