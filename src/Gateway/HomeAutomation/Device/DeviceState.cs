using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.Device
{
    public class DeviceState
    {
        public IDevice Device { get; set; }

        public ComponentState[] ComponentStateList { get; set;}
    }
}
