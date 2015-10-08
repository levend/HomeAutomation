using HomeAutomation.Core;
using MosziNet.HomeAutomation.Gateway.Messaging;
using System.Collections.Generic;

namespace MosziNet.HomeAutomation.Gateway.ApplicationLogic.Messages
{
    public class SendDeviceStateToControllers : IProcessableMessage
    {
        public DeviceState DeviceState { get; set; }

        public void ProcessMessage()
        {
            IList<IHomeController> list = HomeAutomationSystem.ControllerRegistry.Controllers;
            foreach(IHomeController controller in list)
            {
                controller.SendDeviceState(DeviceState);
            }
        }
    }
}
