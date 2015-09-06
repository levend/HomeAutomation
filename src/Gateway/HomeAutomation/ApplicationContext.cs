using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Service;

namespace MosziNet.HomeAutomation
{
    public static class ApplicationContext
    {
        private static ServiceRegistry serviceRegistry;

        static ApplicationContext()
        {
            serviceRegistry = new ServiceRegistry();
        }

        public static ServiceRegistry ServiceRegistry
        {
            get
            {
                return serviceRegistry;
            }
        }
    }
}
