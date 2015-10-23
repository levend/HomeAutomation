using HomeAutomation.Core;
using HomeAutomation.DeviceNetwork.XBee;
using HomeAutomation.Logging;
using HomeAutomation.Util;
using MosziNet.XBee;
using MosziNet.XBee.Frame;
using System;

namespace MosziNet.Devices.XBee
{
    public class DoubleRelayLM35 : RelayDeviceBase, IXBeeDevice
    {
        private static readonly double AnalogPinMaxVoltage = 1200.0; // in millivolts
        private static readonly double AnalogPinResolution = 1024;

        private double temperature;

        /// <summary>
        /// Override base constructor specifying the XBee pins where the relays are connected
        /// </summary>
        public DoubleRelayLM35() : base(ATCommands.D1, ATCommands.D2) { }

        public override void ProcessFrame(IXBeeFrame frame)
        {
            IODataSample dataSample = frame as IODataSample;
            if (dataSample != null)
            {
                if (dataSample.Samples.Length == 4)
                {
                    // first 2 bytes are going to the RelayDevice (digital samples)

                    // next 2 bytes are the analog samples
                    double analogReading = (dataSample.Samples[2] * 256 + dataSample.Samples[3]) * AnalogPinMaxVoltage / AnalogPinResolution;

                    // now calculate the temperature
                    temperature = HomeAutomation.Sensor.Temperature.LM35.TemperatureFromVoltage(analogReading);
                }
                else
                {
                    Log.Debug("[DoubleRelayLM35] Wrong number of samples received: " + HexConverter.ToSpacedHexString(dataSample.Samples));
                }
            }
        }

        public override DeviceState DeviceState
        {
            get
            {
                DeviceState s = base.DeviceState;

                // extend the component state list with 1 item, our temperature
                ComponentState[] states = new ComponentState[s.ComponentStateList.Length + 1];

                states[0] = new ComponentState()
                {
                    Name = "LM35",
                    Value = temperature.ToString("N1")
                };

                // make sure we carry over the existing states
                Array.Copy(s.ComponentStateList, 0, states, 1, s.ComponentStateList.Length);

                return new DeviceState()
                {
                    Device = this,
                    ComponentStateList = states
                };
            }
        }
    }
}
