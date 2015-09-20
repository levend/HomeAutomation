using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Device.Concrete;
using MosziNet.HomeAutomation.ApplicationLogic.MqttDeviceTranslator;
using MosziNet.HomeAutomation.Mqtt;

namespace MosziNet.HomeAutomation.ApplicationLogic.MessageProcessor.XBeeFrameProcessor
{
    public class IODataSampleFrameProcessor : IXBeeFrameProcessor
    {
        public void ProcessFrame(XBee.Frame.IXBeeFrame frame)
        {
            IDeviceTypeRegistry deviceTypeRegistry = (IDeviceTypeRegistry)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(IDeviceTypeRegistry));
            Type deviceType = deviceTypeRegistry.GetDeviceTypeById(frame.Address);
            
            if (deviceType == null)
            {
                // Ask for the type of the device type.
                new DeviceUtil().AskForDeviceType(frame);
            }
            else
            {
                // create the right device
                System.Reflection.ConstructorInfo constructor = deviceType.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    IDevice device = constructor.Invoke(new object[] { }) as IDevice;
                    device.DeviceID = frame.Address;

                    if (device != null)
                    {
                        device.ProcessFrame(frame);

                        // todo: generalize

                        string message = new TemperatureDeviceTranslator().ToMqttMessage(((TemperatureSensor)device));
                        MqttService mqttService = (MqttService)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(MqttService));

                        mqttService.SendMessage("/MosziNet_HA/Status", message);
                    }
                    else
                    {
                        Log.Debug("Device could not be created for type: " + deviceType.Name);
                    }
                }
            }

        }
    }
}
