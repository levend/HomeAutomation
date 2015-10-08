using System;
using System.Reflection;

namespace HomeAutomation.Core
{
    /// <summary>
    /// Provides a basic implementation of the <see cref="IDevice"/> interface.
    /// </summary>
    public abstract class DeviceBase : IDevice
    {
        public byte[] DeviceID { get; set; }

        public byte[] NetworkAddress { get; set; }

        public abstract DeviceState DeviceState { get; }

        public IDeviceNetwork DeviceNetwork { get; internal set; }

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
