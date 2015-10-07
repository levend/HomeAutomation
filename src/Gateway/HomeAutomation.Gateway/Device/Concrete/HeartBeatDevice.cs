using System;
using MosziNet.HomeAutomation.Device.Base;
using MosziNet.HomeAutomation.Messaging;
using MosziNet.HomeAutomation.ApplicationLogic.Messages;

namespace MosziNet.HomeAutomation.Device.Concrete
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
