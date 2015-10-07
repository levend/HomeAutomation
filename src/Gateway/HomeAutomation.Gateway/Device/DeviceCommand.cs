using System;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.Device
{
    /// <summary>
    /// Represents a command that can be executed on devices.
    /// By convention Name represents the name of a method in the device. This should be accessible through reflection. 
    /// Also by convention all methods accessible this way should only have string parameters.
    /// </summary>
    public class DeviceCommand
    {
        /// <summary>
        /// The device that needs to execute the command.
        /// </summary>
        public byte[] DeviceID { get; set; }

        /// <summary>
        /// The name of the command, aka the name of the method to invoke on the device.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The parameters to pass to the device.
        /// </summary>
        public string[] Parameters { get; set; }

        /// <summary>
        /// Builds a device command based on the command message.
        /// Format: methodname,param1,param2,param3,...
        /// </summary>
        /// <param name="commandMessage"></param>
        /// <returns></returns>
        public static DeviceCommand CreateFromString(string commandMessage)
        {
            DeviceCommand command = new DeviceCommand();

            string[] components = commandMessage.Split(',');

            // first parse the address of this command
            byte[] address = HexConverter.BytesFromString(components[0]);

            // copy all items from the components to the parameters starting with index 1
            string[] parameters = new String[components.Length - 2];
            Array.Copy(components, 2, parameters, 0, parameters.Length);

            // build a command structure and return it.
            command.DeviceID = address;
            command.Name = components[1];
            command.Parameters = parameters;

            return command;
        }
    }
}
