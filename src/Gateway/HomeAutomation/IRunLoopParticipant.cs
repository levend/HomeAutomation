using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation
{
    public interface IRunLoopParticipant
    {
        void Execute();
    }
}
