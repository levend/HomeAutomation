using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device.Base;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.Logging;

namespace MosziNet.HomeAutomation.Device.Concrete
{
    public class DoubleRelay : RelayDevice
    {
        public int Switch1State { get; private set; }
        public int Switch2State { get; private set; }

        /// <summary>
        /// Configure this device with D0 and D2 pins.
        /// </summary>
        public DoubleRelay() : base(ATCommands.D0, ATCommands.D2)
        {

        }

        public override void ProcessFrame(XBee.Frame.IXBeeFrame frame)
        {
            IODataSample dataSample = frame as IODataSample;
            if (dataSample != null)
            {
                if (dataSample.Samples.Length == 2)
                {
                    // now read the digital readings, and the temperature sensor reading
                    byte digitalReadingMLB = dataSample.Samples[1];

                    // build the switch states
                    Switch1State = (digitalReadingMLB & 0x04) != 0 ? 0 : 1;
                    Switch2State = (digitalReadingMLB & 0x02) != 0 ? 0 : 1;
                }
                else
                {
                    Log.Debug("[DoubleRelay] Wrong number of samples received: " + HexConverter.ToSpacedHexString(dataSample.Samples));
                }
            }
        }

        public override DeviceState GetDeviceState()
        {
            return new DeviceState()
            {
                Device = this,
                ComponentStateList = new ComponentState[] 
                {
                    new ComponentState() { Name = "Relay1", Value = Switch1State.ToString() },
                    new ComponentState() { Name = "Relay2", Value = Switch2State.ToString() }
                }
            };

        }
    }
}
