using System;

namespace MosziNet.HomeAutomation.Messaging
{
    public interface IProcessableMessage : IMessage
    {
        void ProcessMessage();
    }
}
