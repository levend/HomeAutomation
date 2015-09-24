using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device.Concrete;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.Device;

namespace MosziNet.HomeAutomation.ApplicationLogic.MqttDeviceTranslator
{
    public class TemperatureDevice1Translator : IDeviceTranslator
    {
        public string GetDeviceMessage(IDevice device)
        {
            TemperatureDeviceV1 temperatureDevice = (TemperatureDeviceV1)device;

            return HexConverter.ToHexString(temperatureDevice.DeviceID) + "," + temperatureDevice.Temperature.ToString("N1");
        }

        public void ProcessMessage(IDevice device, string message)
        {

        }
    }
}
