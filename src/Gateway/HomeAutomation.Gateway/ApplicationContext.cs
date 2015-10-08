using System;
using MosziNet.HomeAutomation.Gateway.Service;
using MosziNet.HomeAutomation.Gateway.Configuration;

namespace MosziNet.HomeAutomation
{
    /// <summary>
    /// ApplicationContext is the class where we can access globally needed objects.
    /// </summary>
    public class ApplicationContext
    {
        /// <summary>
        /// The service registry for the application.
        /// </summary>
        public static ServiceRegistry ServiceRegistry { get; set; }

        /// <summary>
        /// Stores the configuration for the application.
        /// </summary>
        public static ApplicationConfiguration Configuration { get; set; }
    }
}
