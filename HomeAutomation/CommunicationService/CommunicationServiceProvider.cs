using System;
using Microsoft.SPOT;
using System.Collections;

namespace MosziNet.HomeAutomation.CommunicationService
{
    /// <summary>
    /// Registers and provides communication services. eg: MQTT, XBee, etc.
    /// </summary>
    public class CommunicationServiceProvider : MosziNet.HomeAutomation.CommunicationService.ICommunicationServiceProvider
    {
        private readonly Hashtable serviceDictionary = new Hashtable();

        public void RegisterCommunicationService(ICommunicationService service, string serviceName)
        {
            serviceDictionary.Add(service, serviceName);
        }

        public ICommunicationService GetService(string serviceName)
        {
            return (ICommunicationService)serviceDictionary[serviceName];
        }
    }
}
