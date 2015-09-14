using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device.Base;

namespace MosziNet.HomeAutomation.Device
{
    public interface IDevice
    {
        string DeviceID { get; set; }
    }
}
