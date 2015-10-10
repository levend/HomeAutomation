using HomeAutomation.Core;
using System;
using System.Collections.Generic;

namespace MosziNet.HomeAutomation.Gateway.Service
{
    /// <summary>
    /// SerivceRegistry is the main point in the application to turn to for services.
    /// </summary>
    public class ServiceRegistry
    {
        private static ServiceRegistry instance = new ServiceRegistry();

        private ServiceRegistry() { }

        public static ServiceRegistry Instance { get { return instance; } }

        private Dictionary<Type, ICooperativeService> serviceRegistry = new Dictionary<Type, ICooperativeService>();

        /// <summary>
        /// Registers a service into the system.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="serviceInstance"></param>
        public void RegisterService(Type serviceType, ICooperativeService serviceInstance)
        {
            serviceRegistry.Add(serviceType, serviceInstance);
        }

        public void RegisterService(ICooperativeService serviceInstance)
        {
            RegisterService(serviceInstance.GetType(), serviceInstance);
        }

        /// <summary>
        /// Returns the requested service from the registry, or null if it is not registered.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public ICooperativeService GetServiceOfType(Type serviceType)
        {
            return serviceRegistry.ContainsKey(serviceType) ? serviceRegistry[serviceType] : null;
        }

        public IList<ICooperativeService> GetServiceList()
        {
            return new List<ICooperativeService>(serviceRegistry.Values);
        }

    }
}
