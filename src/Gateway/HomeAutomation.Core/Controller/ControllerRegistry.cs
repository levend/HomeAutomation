using HomeAutomation.Core.Controller;
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

        private IHomeController[] controllerList = new IHomeController[0];
        private AllControllersController allController;

        public IHomeController All { get { return allController; } }

        public ControllerRegistry()
        {
            allController = new AllControllersController(this);
        }

        /// <summary>
        /// Registers a controller to the system.
        /// </summary>
        /// <param name="controller"></param>
        public void RegisterController(IHomeController controller)
        {
            List<IHomeController> newList = new List<IHomeController>(controllerList);
            newList.Add(controller);

            controllerList = newList.ToArray();

            HomeAutomationSystem.ServiceRegistry.RegisterService(controller);

            ControllerAdded?.Invoke(this, controller);
        }

        /// <summary>
        /// Returns the currently registered controller list, which can be used to loop through the list, as it will never change, but 
        /// rather a new instance will be returned on change.
        /// </summary>
        public IHomeController[] GetControllers()
        {
            return controllerList;
        }
    }
}
