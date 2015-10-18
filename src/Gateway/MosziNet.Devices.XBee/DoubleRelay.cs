using MosziNet.XBee.Frame.ZigBee;

namespace MosziNet.Devices.XBee
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
