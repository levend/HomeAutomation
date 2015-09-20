using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Service;

namespace MosziNet.HomeAutomation
{
    /// <summary>
    /// ApplicationContext is the class where we can access globally needed objects.
    /// </summary>
    public static class ApplicationContext
    {
        private static ServiceRegistry serviceRegistry;

        static ApplicationContext()
        {
            serviceRegistry = new ServiceRegistry();
        }

        /// <summary>
        /// The service registry for the application.
        /// </summary>
        public static ServiceRegistry ServiceRegistry
        {
            get
            {
                return serviceRegistry;
            }
        }
    }
}
