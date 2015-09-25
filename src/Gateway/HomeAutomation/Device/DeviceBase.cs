using System;
using Microsoft.SPOT;
using System.Reflection;

namespace MosziNet.HomeAutomation.Device.Base
{
    public abstract class DeviceBase : IDevice
    {
        public byte[] DeviceID { get; set; }

        public abstract void ProcessFrame(XBee.Frame.IXBeeFrame frame);

        public abstract DeviceState GetDeviceState();

        /// <summary>
        /// Executes the command on this instance. 
        /// More precisely this invokes the aCommand.Name method with the specified parameters.
        /// </summary>
        /// <param name="aCommand"></param>
        public void ExecuteCommand(DeviceCommand aCommand)
        {
            MethodInfo executableMethod = this.GetType().GetMethod(aCommand.Name);
            executableMethod.Invoke(this, aCommand.Parameters);
        }
    }
}
