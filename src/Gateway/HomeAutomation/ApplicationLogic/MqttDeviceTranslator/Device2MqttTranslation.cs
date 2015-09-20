using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.ApplicationLogic.MqttDeviceTranslator;
using MosziNet.HomeAutomation.Device;
using System.Collections;
using MosziNet.HomeAutomation.Device.Concrete;

namespace MosziNet.HomeAutomation.ApplicationLogic
{
    public static class Device2MqttTranslation
    {
        private static Hashtable deviceTranslators = new Hashtable();

        static Device2MqttTranslation()
        {
            deviceTranslators.Add(typeof(TemperatureSensor), typeof(TemperatureDeviceTranslator));
            deviceTranslators.Add(typeof(HeartBeatDevice), typeof(HeartBeatDeviceTranslator));
        }

        public static IDeviceTranslator GetTranslator(IDevice device)
        {
            IDeviceTranslator translator = null;

            Type deviceType = device.GetType();
            Type traslatorType = deviceTranslators.Contains(deviceType) ? (Type)deviceTranslators[deviceType] : null;

            if (traslatorType != null)
            {
                translator = (IDeviceTranslator)traslatorType.GetConstructor(new Type[] { }).Invoke(new object[] { });
            }

            return translator;
        }
    }
}
