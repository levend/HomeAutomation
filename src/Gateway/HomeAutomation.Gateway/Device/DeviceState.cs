using System;
using System.Text;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.Device
{
    /// <summary>
    /// Contains the state of a device enlisting all it's component's states.
    /// </summary>
    public class DeviceState
    {
        /// <summary>
        /// The device itself.
        /// </summary>
        public IDevice Device { get; set; }

        /// <summary>
        /// The list of the components in this device.
        /// </summary>
        public ComponentState[] ComponentStateList { get; set;}

        /// <summary>
        /// Serializes the device status into a string message.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public string ConvertToString()
        {
            StringBuilder stateBuilder = new StringBuilder();

            stateBuilder.Append(HexConverter.ToHexString(this.Device.DeviceID));

            foreach (ComponentState componentState in this.ComponentStateList)
            {
                // add trailing comma after previous state component
                stateBuilder.Append(",");

                // append the sensor name and the value separated by comma
                stateBuilder.Append(componentState.Name);
                stateBuilder.Append(",");
                stateBuilder.Append(componentState.Value);
            }

            return stateBuilder.ToString();
        }

    }
}
