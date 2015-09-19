using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.Device.Base
{
    public abstract class DeviceBase : IDevice
    {
        public byte[] DeviceID { get; set; }

        public abstract void ProcessFrame(XBee.Frame.IXBeeFrame frame);
    }
}
