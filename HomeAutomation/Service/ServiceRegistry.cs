using System;
using Microsoft.SPOT;
using System.Collections;

namespace MosziNet.HomeAutomation.Service
{
    public class ServiceRegistry
    {
        private Hashtable serviceRegistry = new Hashtable();

        public void RegisterService(Type serviceType, object serviceInstance)
        {
            serviceRegistry.Add(serviceType, serviceInstance);
        }

        public object GetServiceOfType(Type serviceType)
        {
            return serviceRegistry.Contains(serviceType) ? serviceRegistry[serviceType] : null;
        }
    }
}
