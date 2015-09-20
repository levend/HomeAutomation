using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.Device.Concrete;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using MosziNet.HomeAutomation.ApplicationLogic.Messages;
using MosziNet.HomeAutomation.Messaging;
using MosziNet.HomeAutomation.Logging;

namespace MosziNet.HomeAutomation.Device
{
    public class DeviceUtil
    {
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
                Log.Debug("Device with type ID " + HexConverter.ToHexString(responseFrame.Address) + " is not known.");
            }

            return device;
        }
    }
}
