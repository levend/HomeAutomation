using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.Device.Concrete;
using MosziNet.HomeAutomation.Converter;

namespace MosziNet.HomeAutomation.Device
{
    public class DeviceUtil
    {
        public void AskForDeviceType(byte[] address)
        {
            // todo: build correct frame, put message to the message bus
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
