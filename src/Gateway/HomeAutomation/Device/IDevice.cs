using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device.Base;
using MosziNet.HomeAutomation.XBee.Frame;

namespace MosziNet.HomeAutomation.Device
{
    public interface IDevice
    {
        byte[] DeviceID { get; set; }

        void ProcessFrame(IXBeeFrame frame);

        DeviceState GetDeviceState();
    }
}
