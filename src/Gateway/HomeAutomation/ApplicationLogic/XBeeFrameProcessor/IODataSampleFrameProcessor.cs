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
using MosziNet.HomeAutomation.Configuration;
using MosziNet.HomeAutomation.XBee;

namespace MosziNet.HomeAutomation.ApplicationLogic.XBeeFrameProcessor
{
    /// <summary>
    /// Processes the IO data samples coming from the XBee network.
    /// </summary>
    public class IODataSampleFrameProcessor : IXBeeFrameProcessor
    {
        static byte frameId;

        public void ProcessFrame(XBee.Frame.IXBeeFrame frame)
        {
            // get the device type from device registry. if it's not found
            // then we will ask the device to identify itself.
            DeviceTypeRegistry deviceTypeRegistry = (DeviceTypeRegistry)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(DeviceTypeRegistry));
            Type deviceType = deviceTypeRegistry.GetDeviceTypeById(frame.Address);
            
            if (deviceType != null)
            {
                ProcessFrameByDevice(frame, deviceType);
            }
            else
            {
                ProcessFrameForUnknownDevice(frame);
            }
        }

        private void ProcessFrameByDevice(XBee.Frame.IXBeeFrame frame, Type deviceType)
        {
            IDevice device = Activator.CreateInstance(deviceType) as IDevice;
            if (device != null)
            {
                device.DeviceID = frame.Address;

                // first process the frame by the device
                device.ProcessFrame(frame);

                PostDeviceStateToMessageBus(device);
            }
            else
            {
                Log.Debug("Device could not be created for type: " + deviceType.Name);
            }
        }

        private void PostDeviceStateToMessageBus(IDevice device)
        {
            // convert the device frame to an mqtt message
            string message = DeviceState2MQTTTranslator.GetDeviceMessage(device);

            IMessageBus messageBus = (IMessageBus)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(IMessageBus));

            // now post the message to the message bus
            messageBus.PostMessage(new SendMessageToMqtt()
            {
                Message = message,
                TopicSuffix = MqttTopic.StatusTopic
            });
        }

        private void ProcessFrameForUnknownDevice(IXBeeFrame frame)
        {
            AskForDeviceType(frame);
        }

        private void AskForDeviceType(IXBeeFrame remoteFrame)
        {
            frameId++;
            if (frameId == 0) frameId++;

            Log.Debug("Received a frame from an unknown device, so we are asking type ID from this device. Address: " + HexConverter.ToHexString(remoteFrame.Address));

            // create the XBee frame to send
            IXBeeFrame frame = new XBeeFrameBuilder().CreateRemoteATCommand(ATCommands.DD, frameId, remoteFrame.Address, remoteFrame.NetworkAddress);

            // post this message to the device
            IMessageBus messageBus = (IMessageBus)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(IMessageBus));
            messageBus.PostMessage(new SendFrameToXBee(frame));
        }
    }
}
