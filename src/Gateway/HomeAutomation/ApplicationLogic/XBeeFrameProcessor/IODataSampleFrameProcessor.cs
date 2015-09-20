using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Device.Concrete;
using MosziNet.HomeAutomation.ApplicationLogic.MqttDeviceTranslator;
using MosziNet.HomeAutomation.Mqtt;
using MosziNet.HomeAutomation.ApplicationLogic.Messages;
using MosziNet.HomeAutomation.Messaging;

namespace MosziNet.HomeAutomation.ApplicationLogic.XBeeFrameProcessor
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
                        // first process the frame by the device
                        device.ProcessFrame(frame);

                        TranslateDeviceToMqttMessage(device);
                    }
                    else
                    {
                        Log.Debug("Device could not be created for type: " + deviceType.Name);
                    }
                }
            }
        }

        private static void TranslateDeviceToMqttMessage(IDevice device)
        {
            IDeviceTranslator deviceTranslator = (IDeviceTranslator)Device2MqttTranslation.GetTranslator(device);
            IMessageBus messageBus = (IMessageBus)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(IMessageBus));

            if (deviceTranslator != null)
            {
                PostDeviceMessageToBus(device, deviceTranslator, messageBus);
            }
            else
            {
                Log.Error("Device with address " + HexConverter.ToHexString(device.DeviceID) + " can not be translated.");
            }
        }

        private static void PostDeviceMessageToBus(IDevice device, IDeviceTranslator deviceTranslator, IMessageBus messageBus)
        {
            // convert the device frame to an mqtt message
            string message = deviceTranslator.GetDeviceMessage(device);

            // now post the message to the message bus
            messageBus.PostMessage(new PostToMqttMessage()
            {
                Message = message,
                TopicSuffix = MqttConfiguration.StatusTopic
            });
        }
    }
}
