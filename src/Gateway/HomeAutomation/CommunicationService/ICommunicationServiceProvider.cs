using System;

namespace MosziNet.HomeAutomation.CommunicationService
{
    public interface ICommunicationServiceProvider
    {
        ICommunicationService GetService(string serviceName);
        void RegisterCommunicationService(ICommunicationService service, string serviceName);
    }
}
