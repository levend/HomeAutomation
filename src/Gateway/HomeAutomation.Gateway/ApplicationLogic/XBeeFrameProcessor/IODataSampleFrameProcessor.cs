using System;
using MosziNet.HomeAutomation.Device;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Device.Concrete;
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
        DeviceRegistry deviceRegistry;
        static byte frameId;

        public void ProcessFrame(XBee.Frame.IXBeeFrame frame)
        {
            if (this.deviceRegistry == null)
            {
                deviceRegistry = (DeviceRegistry)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(DeviceRegistry));
            }

            // get the device type from device registry. if it's not found
            // then we will ask the device to identify itself.
            IDevice device = deviceRegistry.GetDeviceById(frame.Address);

            if (device != null)
            {
                ProcessFrameByDevice(frame, device);
            }
            else
            {
                ProcessFrameForUnknownDevice(frame);
            }
        }

        private void ProcessFrameByDevice(XBee.Frame.IXBeeFrame frame, IDevice device)
        {
            // first process the frame by the device
            device.ProcessFrame(frame);

            PostDeviceStateToMessageBus(device);
        }

        private void PostDeviceStateToMessageBus(IDevice device)
        {
            // convert the device frame to an mqtt message
            string message = device.GetDeviceState().ConvertToString();

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
            // check if this device is already staging
            if (!deviceRegistry.IsStagingDevice(frame.Address))
            {
                deviceRegistry.RegisterStagingDevice(frame.Address);

                AskForDeviceType(frame);
            }
        }

        private void AskForDeviceType(IXBeeFrame remoteFrame)
        {
            frameId++;
            if (frameId == 0) frameId++;

            Log.Debug($"Received a frame from an unknown device, so we are asking type ID from this device. Address: {remoteFrame.Address.ToHexString()}");

            // create the XBee frame to send
            IXBeeFrame frame = XBeeFrameBuilder.CreateRemoteATCommand(ATCommands.DD, frameId, remoteFrame.Address, remoteFrame.NetworkAddress);

            // post this message to the device
            IMessageBus messageBus = (IMessageBus)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(IMessageBus));
            messageBus.PostMessage(new SendFrameToXBee(frame));
        }
    }
}
