using System;

namespace MosziNet.HomeAutomation.Gateway.Messaging
{
    public interface IProcessableMessage : IMessage
    {
        void ProcessMessage();
    }
}
