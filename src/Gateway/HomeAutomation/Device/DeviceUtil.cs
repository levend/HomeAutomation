using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.Device.Concrete;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using MosziNet.HomeAutomation.ApplicationLogic.Messages;

namespace MosziNet.HomeAutomation.Device
{
    public class DeviceUtil
    {
        static byte frameId;

        public void AskForDeviceType(IXBeeFrame remoteFrame)
        {
            frameId++;
            if (frameId == 0)
                frameId++;

            Debug.Print("Received a frame from an unknown device, so we are asking type ID from this device. Address: " + HexConverter.ToHexString(remoteFrame.Address));

            // build the frame to ask the device type id
            RemoteATCommand frame = new RemoteATCommand();
            frame.Address = remoteFrame.Address;
            frame.NetworkAddress = remoteFrame.NetworkAddress;

            frame.ATCommand = ATCommands.DD;
            frame.FrameId = frameId;

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
            RemoteCommandResponse responseFrame = (RemoteCommandResponse)frame;
            int deviceIdentification = responseFrame.Parameters[2] * 256 + responseFrame.Parameters[3];
            IDevice device = null;

            if (deviceIdentification == 0x9988)
            {
                device = new TemperatureSensor();

                device.DeviceID = frame.Address;
            }

            if (deviceIdentification == 0x9987)
            {
                device = new HeartBeatDevice();
                device.DeviceID = frame.Address;
            }

            if (device == null)
            {
                Debug.Print("Device with type ID " + HexConverter.ToHexString(responseFrame.Address) + " is not known.");
            }

            return device;
        }
    }
}
