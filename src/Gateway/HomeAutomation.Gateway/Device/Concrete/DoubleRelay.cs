using System;
using MosziNet.HomeAutomation.Gateway.Device.Base;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.Logging;

namespace MosziNet.HomeAutomation.Gateway.Device.Concrete
{
    public class DoubleRelay : RelayDeviceBase
    {
        /// <summary>
        /// Configure this device with D0 and D2 pins.
        /// </summary>
        public DoubleRelay() : base(ATCommands.D0, ATCommands.D2)
        {

        }
    }
}
