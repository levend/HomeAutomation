using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device.Concrete;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.ApplicationLogic.MqttDeviceTranslator
{
    public class TemperatureDeviceTranslator
    {
        public string ToMqttMessage(TemperatureSensor temperatureDevice)
        {
            return HexConverter.ToHexString(temperatureDevice.DeviceID) + "," + temperatureDevice.Temperature.ToString("N1");
        }
    }
}
