using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.Device.Concrete;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Configuration;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.ApplicationLogic.XBeeFrameProcessor
{
    public class RemoteCommandResponseProcessor : IXBeeFrameProcessor
    {
        public void ProcessFrame(XBee.Frame.IXBeeFrame frame)
        {
            RemoteCommandResponse responseFrame = (RemoteCommandResponse)frame;

            // check if this is a DD command response - used for identifying devices by their type
            if (FrameUtil.IsSameATCommand(responseFrame.ATCommand, ATCommands.DD))
            {
                DeviceTypeRegistry deviceTypeRegistry = (DeviceTypeRegistry)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(DeviceTypeRegistry));

                // build the ID for the device type based on the frame info
                int deviceIdentification = responseFrame.Parameters[2] * 256 + responseFrame.Parameters[3];

                // get the type from the configuration based on this type id
                Type deviceType = ApplicationConfiguration.GetTypeForKey(ApplicationConfigurationCategory.DeviceTypeID, deviceIdentification);

                // create the device and register it, if possible
                IDevice device = Activator.CreateInstance(deviceType) as IDevice;
                if (device != null)
                {
                    deviceTypeRegistry.RegisterDevice(device.GetType(), device.DeviceID);
                }
                else
                {
                    Log.Debug("The device type specified by the sensor with ID " + HexConverter.ToHexString(frame.Address) + " is not know. Type ID: " + HexConverter.ToSpacedHexString(responseFrame.Parameters));

                    deviceTypeRegistry.RegisterDevice(typeof(UnknownDevice), frame.Address);
                }
            }
        }
    }
}
