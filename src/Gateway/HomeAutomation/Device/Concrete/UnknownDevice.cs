using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device.Base;

namespace MosziNet.HomeAutomation.Device.Concrete
{
    public class UnknownDevice : DeviceBase
    {
        public override void ProcessFrame(XBee.Frame.IXBeeFrame frame)
        {
            Debug.Print("An unknown device received a frame.");
        }
    }
}
