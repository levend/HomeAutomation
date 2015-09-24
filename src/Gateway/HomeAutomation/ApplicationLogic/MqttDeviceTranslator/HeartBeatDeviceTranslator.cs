using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Messaging;
using MosziNet.HomeAutomation.ApplicationLogic.Messages;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.ApplicationLogic.MqttDeviceTranslator
{
    internal class HeartBeatDeviceTranslator : IDeviceTranslator
    {
        private static uint counter = 0;

        public string GetDeviceMessage(Device.IDevice device)
        {
            return HexConverter.ToHexString(device.DeviceID) + "," + counter;
        }

        public void ProcessMessage(Device.IDevice device, string message)
        {
            
        }
    }
}
