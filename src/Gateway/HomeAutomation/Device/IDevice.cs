using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.Device
{
    public interface IDevice
    {
        string DeviceID { get; }

        string[] SensorList { get; }

        void ProcessCommand(DeviceCommand command);
    }
}
