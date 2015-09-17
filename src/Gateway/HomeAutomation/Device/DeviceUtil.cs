using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.Device.Concrete;
using MosziNet.HomeAutomation.BusinessLogic.Messages;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;

namespace MosziNet.HomeAutomation.Device
{
    public class DeviceUtil
    {
        public void AskForDeviceType(byte[] address)
        {
            // build the frame to ask the device type id
            RemoteATCommand frame = new RemoteATCommand();
            frame.Address = address;
            frame.ATCommand = ATCommands.DD;

            // post this message to the device
            IMessageBus messageBus = (IMessageBus) ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(IMessageBus));
            messageBus.PostMessage(new DeviceCommandMessage(frame));
        }

        /// <summary>
        /// Create a device by using type information from the frame. The frame should be a device
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public IDevice CreateDeviceByDeviceTypeInFrame(IXBeeFrame frame)
        {
            // TODO: check the DD type in the frame, crate the correct device type
            IDevice device = new TemperatureSensor();

            device.DeviceID = HexConverter.ToHexString(frame.Address);

            return device;
        }
    }
}
