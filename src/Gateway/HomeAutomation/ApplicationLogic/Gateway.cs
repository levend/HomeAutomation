using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.Mqtt;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.BusinessLogic
{
    public class Gateway
    {
        XBeeService xbeeService;
        IDeviceTypeRegistry deviceRegistry;

        public Gateway()
        {
            deviceRegistry = (IDeviceTypeRegistry)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(IDeviceTypeRegistry));
            xbeeService = (XBeeService)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(XBeeService));

            // subscribe to the messages coming from that network.
            xbeeService.MessageReceived += xbeeService_MessageReceived;
        }

        void xbeeService_MessageReceived(XBee.Frame.IXBeeFrame frame)
        {
            IXBeeDevice device;

            if (frame.FrameType == XBee.Frame.FrameType.RemoteCommandResponse)
            {
                // TODO check if this indeed is a device identification response

                // the device answered for our identification request, so create the device and register it
                device = new DeviceUtil().CreateDeviceByDeviceTypeInFrame(frame) as IXBeeDevice;
                if (device != null)
                {
                    deviceRegistry.RegisterDevice(device.GetType(), device.DeviceID);       
                }
                else 
                {
                    Debug.Print("Device created based on the DD response is not valid.");
                }
            }
            else
            {
                Type deviceType = deviceRegistry.GetDeviceTypeById(HexConverter.ToHexString(frame.Address));
                if (deviceType == null)
                {
                    Debug.Print("Received a frame from an unknown device with address: " + HexConverter.ToHexString(frame.Address));

                    // Ask for the type of the device type.
                    new DeviceUtil().AskForDeviceType(frame.Address);
                }
                else
                {
                    // received a frame for a known device type, so create that device and process the frame

                    // create the right device
                    System.Reflection.ConstructorInfo constructor = deviceType.GetConstructor(new Type[] { });
                    if (constructor != null)
                    {
                        device = constructor.Invoke(new object[] { }) as IXBeeDevice;
                        if (device != null)
                        {
                            device.ProcessFrame(frame);
                        }
                        else
                        {
                            Debug.Print("Device could not be created.");
                        }
                    }
                }
            }
        }
    }
}
