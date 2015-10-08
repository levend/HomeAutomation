using HomeAutomation.Core;
using MosziNet.HomeAutomation.XBee.Frame;

namespace HomeAutomation.DeviceNetwork.XBee.Device
{
    public class FakeTemperatureDevice : DeviceBase
    {
        public override DeviceState DeviceState
        {
            get
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
        }

        public void ProcessFrame(IXBeeFrame frame)
        {
            
        }
    }
}
