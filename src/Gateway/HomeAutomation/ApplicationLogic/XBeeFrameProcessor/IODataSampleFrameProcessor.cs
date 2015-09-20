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
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;

namespace MosziNet.HomeAutomation.ApplicationLogic.XBeeFrameProcessor
{
    public class IODataSampleFrameProcessor : IXBeeFrameProcessor
    {
        static byte frameId;

        public void ProcessFrame(XBee.Frame.IXBeeFrame frame)
        {
            IDeviceTypeRegistry deviceTypeRegistry = (IDeviceTypeRegistry)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(IDeviceTypeRegistry));
            Type deviceType = deviceTypeRegistry.GetDeviceTypeById(frame.Address);
            
            if (deviceType != null)
            {
                ProcessFrameByDevice(frame, deviceType);
            }
            else
            {
                AskForDeviceType(frame);
            }
        }

        private void ProcessFrameByDevice(XBee.Frame.IXBeeFrame frame, Type deviceType)
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

        private void TranslateDeviceToMqttMessage(IDevice device)
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

        private void PostDeviceMessageToBus(IDevice device, IDeviceTranslator deviceTranslator, IMessageBus messageBus)
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

        private void AskForDeviceType(IXBeeFrame remoteFrame)
        {
            frameId++;
            if (frameId == 0)
                frameId++;

            Log.Debug("Received a frame from an unknown device, so we are asking type ID from this device. Address: " + HexConverter.ToHexString(remoteFrame.Address));

            // build the frame to ask the device type id
            RemoteATCommand frame = new RemoteATCommand();
            frame.Address = remoteFrame.Address;
            frame.NetworkAddress = remoteFrame.NetworkAddress;

            frame.ATCommand = ATCommands.DD;
            frame.FrameId = frameId;

            // post this message to the device
            IMessageBus messageBus = (IMessageBus)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(IMessageBus));
            messageBus.PostMessage(new DeviceCommandMessage(frame));
        }

    }
}
