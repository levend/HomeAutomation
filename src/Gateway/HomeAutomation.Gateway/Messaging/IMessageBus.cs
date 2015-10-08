using System;

namespace MosziNet.HomeAutomation.Gateway.Messaging
{
    // Declare the delegate (if using non-generic pattern). 
    public delegate void MessageArrivedDelegate(IMessage message);

    public interface IMessageBus
    {
        IMessageBusRunner MessageBusRunner { get; set; }

        void PostMessage(IMessage message);
        IMessage DequeueMessage();
    }
}
