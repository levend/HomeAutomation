using System;
using MosziNet.HomeAutomation.Gateway.Device.Base;
using MosziNet.HomeAutomation.Gateway.Messaging;
using MosziNet.HomeAutomation.Gateway.ApplicationLogic.Messages;

namespace MosziNet.HomeAutomation.Gateway.Device.Concrete
{
    public class HeartBeatDevice : DeviceBase
    {
        public override void ProcessFrame(XBee.Frame.IXBeeFrame frame)
        {
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
