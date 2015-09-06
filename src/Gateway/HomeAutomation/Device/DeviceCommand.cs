using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.Device
{
    public class DeviceCommand
    {
        public string DeviceId { get; set; }

        public string SensorId { get; set; }

        public string Command { get; set; }
    }
}
