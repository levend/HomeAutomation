using System.Collections.Generic;

namespace HomeAutomation.Core.Service
{
    /// <summary>
    /// SerivceRegistry is the main point in the application to turn to for services.
    /// </summary>
    public class ServiceRegistry
    {
        public ServiceRunner Runner { get; private set; } = new ServiceRunner();

        private List<ICooperativeService> serviceRegistry = new List<ICooperativeService>();

        /// <summary>
        /// Registers a service into the system.
        /// </summary>
        /// <param name="serviceInstance"></param>
        public void RegisterService(ICooperativeService serviceInstance)
        {
            serviceRegistry.Add(serviceInstance);
        }

        public IList<ICooperativeService> GetServiceList()
        {
            return serviceRegistry;
        }

    }
}
