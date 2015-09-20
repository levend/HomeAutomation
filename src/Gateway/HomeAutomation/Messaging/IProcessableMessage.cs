using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.Messaging
{
    public interface IProcessableMessage : IMessage
    {
        void ProcessMessage();
    }
}
