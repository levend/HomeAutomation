using System;
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
            if (XBeeFrameUtil.IsSameATCommand(responseFrame.ATCommand, ATCommands.DD))
            {
                // build the ID for the device type based on the frame info
                int deviceIdentification = responseFrame.Parameters[2] * 256 + responseFrame.Parameters[3];

                DeviceRegistry deviceRegistry = (DeviceRegistry)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(DeviceRegistry));

                deviceRegistry.RegisterDeviceWithTypeID(deviceIdentification, frame.Address, frame.NetworkAddress);
            }
            else
            {
                DeviceRegistry deviceRegistry = (DeviceRegistry)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(DeviceRegistry));
                
                // get the device type from device registry. if it's not found
                // then we will ask the device to identify itself.
                IDevice device = deviceRegistry.GetDeviceById(frame.Address);

                if (device != null)
                {
                    // first process the frame by the device
                    device.ProcessFrame(frame);
                }
            }
        }
    }
}
