using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation
{
    public interface IMessageProcessor
    {
        void ProcessMessage(Message message);
    }
}
