using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.Device.Base
{
    public abstract class DeviceBase : IDevice
    {
        public string DeviceID { get; set; }
    }
}
