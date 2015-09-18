using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.ApplicationLogic.MessageProcessor.XBeeFrameProcessor
{
    public class IODataSampleFrameProcessor : IXBeeFrameProcessor
    {
        public void ProcessFrame(XBee.Frame.IXBeeFrame frame)
        {
            string hexAddress = HexConverter.ToHexString(frame.Address);
            IDeviceTypeRegistry deviceTypeRegistry = (IDeviceTypeRegistry)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(IDeviceTypeRegistry));
            Type deviceType = deviceTypeRegistry.GetDeviceTypeById(hexAddress);
            
            if (deviceType == null)
            {
                Debug.Print("Received a frame from an unknown device with address: " + hexAddress);

                // Ask for the type of the device type.
                new DeviceUtil().AskForDeviceType(frame);
            }
            else
            {
                // create the right device
                System.Reflection.ConstructorInfo constructor = deviceType.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    IXBeeDevice device = constructor.Invoke(new object[] { }) as IXBeeDevice;
                    if (device != null)
                    {
                        device.ProcessFrame(frame);
                    }
                    else
                    {
                        Debug.Print("Device could not be created for type: " + deviceType.Name);
                    }
                }
            }

        }
    }
}
