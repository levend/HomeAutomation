using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device.Base;

namespace MosziNet.HomeAutomation.Device.Concrete
{
    public class HeartBeatDevice : DeviceBase
    {
        public override void ProcessFrame(XBee.Frame.IXBeeFrame frame)
        {
            Debug.Print("[HearBeatDevice] I'm here !");
        }
    }
}
