using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device;
using System.Text;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.ApplicationLogic.MqttDeviceTranslator
{
    /// <summary>
    /// This interface is the bases of converting between device data and serialized messages.
    /// </summary>
    public static class DeviceState2MQTTTranslator
    {
        /// <summary>
        /// Serializes the device status into a string message.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static string GetDeviceMessage(IDevice device)
        {
            DeviceState state = device.GetDeviceState();

            StringBuilder stateBuilder = new StringBuilder();

            stateBuilder.Append(HexConverter.ToHexString(state.Device.DeviceID));
            
            foreach(ComponentState componentState in state.ComponentStateList)
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
