using System;
using MosziNet.HomeAutomation.Device.Base;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using MosziNet.HomeAutomation.Logging;

namespace MosziNet.HomeAutomation.Device.Concrete
{
    /// <summary>
    /// This temperature device uses an MCP9700 (or compatible) sensor to measure temperature.
    /// </summary>
    public class TemperatureDeviceV2 : DeviceBase
    {
        private static readonly double AnalogPinMaxVoltage = 1200.0; // in millivolts
        private static readonly double AnalogPinResolution = 1024;

        public double Temperature { get; private set; }

        public override void ProcessFrame(XBee.Frame.IXBeeFrame frame)
        {
            IODataSample dataSample = frame as IODataSample;
            if (dataSample != null)
            {
                if (dataSample.Samples.Length == 2)
                {
                    // now read the temperature sensor reading
                    double analogReading = (dataSample.Samples[0] * 256 + dataSample.Samples[1]) * AnalogPinMaxVoltage / AnalogPinResolution;

                    Temperature = HomeAutomation.Sensor.Temperature.LM35.TemperatureFromVoltage(analogReading);
                }
                else
                {
                    Log.Debug("[TemperatureSensor] Wrong number of samples received.");
                }
            }
            else
            {
                Log.Debug("[TemperatureSensor] Wrong frame type (or null) for temperature sensor device.");
            }
        }

        public override DeviceState GetDeviceState()
        {
            return new DeviceState()
            {
                Device = this,
                ComponentStateList = new ComponentState[] 
                {
                    new ComponentState() { Name = "LM35", Value = Temperature.ToString("N1") }
                }
            };
        }
    }
}
