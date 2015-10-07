using System;
using MosziNet.HomeAutomation.Service;
using MosziNet.HomeAutomation.Configuration;

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
