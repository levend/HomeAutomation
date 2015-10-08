using System;
using System.Text;

namespace MosziNet.HomeAutomation.Gateway.Messaging
{
    public interface IMessageProcessor
    {
        void ProcessMessage(IMessage message);
    }
}
