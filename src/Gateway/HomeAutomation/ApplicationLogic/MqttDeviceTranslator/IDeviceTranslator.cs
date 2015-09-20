using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device;

namespace MosziNet.HomeAutomation.ApplicationLogic.MqttDeviceTranslator
{
    public interface IDeviceTranslator
    {
        string GetDeviceMessage(IDevice device);

        void ProcessMessage(IDevice device, string message);
    }
}
