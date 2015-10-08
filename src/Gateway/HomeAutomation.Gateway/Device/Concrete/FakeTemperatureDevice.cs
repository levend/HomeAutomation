using MosziNet.HomeAutomation.Gateway.Device.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MosziNet.HomeAutomation.Gateway.Device;
using MosziNet.HomeAutomation.XBee.Frame;

namespace MosziNet.HomeAutomation.Gateway.Device.Concrete
{
    public class FakeTemperatureDevice : DeviceBase
    {
        public override DeviceState GetDeviceState()
        {
            return new DeviceState()
            {
                Device = this,
                ComponentStateList = new ComponentState[]
                 {
                     new ComponentState() { Name = "FAKETEMPERATURE", Value= "23.5" }
                 }
            };
        }

        public override void ProcessFrame(IXBeeFrame frame)
        {
            
        }
    }
}
