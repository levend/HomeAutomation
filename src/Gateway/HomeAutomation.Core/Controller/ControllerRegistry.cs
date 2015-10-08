using System;
using System.Collections.Generic;

namespace HomeAutomation.Core
{
    /// <summary>
    /// Maintains a registry of the controllers registered to the system.
    /// </summary>
    public class ControllerRegistry
    {
        public event EventHandler<IHomeController> ControllerAdded;

        private IList<IHomeController> controllerList = new List<IHomeController>();

        /// <summary>
        /// Registers a controller to the system.
        /// </summary>
        /// <param name="controller"></param>
        public void RegisterController(IHomeController controller)
        {
            List<IHomeController> newList = new List<IHomeController>(controllerList);
            newList.Add(controller);
            
            controllerList = newList.AsReadOnly();

            ControllerAdded?.Invoke(this, controller);
        }

        /// <summary>
        /// Returns the currently registered controller list.
        /// </summary>
        public IList<IHomeController> Controllers
        {
            get
            {
                return controllerList;
            }
        }
    }
}
