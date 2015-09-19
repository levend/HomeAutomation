using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.Device.Concrete;

namespace MosziNet.HomeAutomation.ApplicationLogic.MessageProcessor.XBeeFrameProcessor
{
    class RemoteCommandResponseProcessor : IXBeeFrameProcessor
    {
        public void ProcessFrame(XBee.Frame.IXBeeFrame frame)
        {
            RemoteCommandResponse responseFrame = (RemoteCommandResponse)frame;

            // check if this is a DD command response - used for identifying devices by their type
            if (FrameUtil.IsSameATCommand(responseFrame.ATCommand, ATCommands.DD))
            {
                IDeviceTypeRegistry deviceTypeRegistry = (IDeviceTypeRegistry)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(IDeviceTypeRegistry));

                // the device answered for our identification request, so create the device and register it
                IDevice device = new DeviceUtil().CreateDeviceByDeviceTypeInFrame(frame) as IDevice;
                if (device != null)
                {
                    deviceTypeRegistry.RegisterDevice(device.GetType(), device.DeviceID);
                }
                else
                {
                    Debug.Print("Device created based on the DD response is not valid, storing it as an 'Unknown' device.");

                    deviceTypeRegistry.RegisterDevice(typeof(UnknownDevice), frame.Address);
                }

            }
        }
    }
}
