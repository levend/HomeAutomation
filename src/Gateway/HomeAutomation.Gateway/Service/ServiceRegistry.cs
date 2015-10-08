using System;
using System.Collections.Generic;

namespace MosziNet.HomeAutomation.Service
{
    /// <summary>
    /// SerivceRegistry is the main point in the application to turn to for services.
    /// </summary>
    public class ServiceRegistry
    {
        private Dictionary<Type, object> serviceRegistry = new Dictionary<Type, object>();

        /// <summary>
        /// Registers a service into the system.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="serviceInstance"></param>
        public void RegisterService(Type serviceType, object serviceInstance)
        {
            serviceRegistry.Add(serviceType, serviceInstance);
        }

        /// <summary>
        /// Returns the requested service from the registry, or null if it is not registered.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetServiceOfType(Type serviceType)
        {
            return serviceRegistry.ContainsKey(serviceType) ? serviceRegistry[serviceType] : null;
        }
    }
}
