using System;
using System.Text;

namespace MosziNet.HomeAutomation.Messaging
{
    public interface IMessageProcessor
    {
        void ProcessMessage(IMessage message);
    }
}
