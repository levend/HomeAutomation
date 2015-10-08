using System;
using MosziNet.HomeAutomation.Gateway.Device.Base;
using MosziNet.HomeAutomation.Logging;

namespace MosziNet.HomeAutomation.Gateway.Device.Concrete
{
    public class UnknownDevice : DeviceBase
    {
        public override void ProcessFrame(XBee.Frame.IXBeeFrame frame)
        {
            Log.Debug("An unknown device received a frame.");
        }

        public override DeviceState GetDeviceState()
        {
            return new DeviceState()
            {
                Device = this,
                ComponentStateList = new ComponentState[] { }
            };
        }
    }
}
