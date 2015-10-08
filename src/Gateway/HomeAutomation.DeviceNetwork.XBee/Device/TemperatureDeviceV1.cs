using HomeAutomation.Core;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;

namespace HomeAutomation.DeviceNetwork.XBee.Device
{
    /// <summary>
    /// This temperature device uses an MCP9700 (or compatible) sensor to measure temperature.
    /// </summary>
    public class TemperatureDeviceV1 : DeviceBase, IXBeeDevice
    {
        private static readonly double AnalogPinMaxVoltage = 1200.0; // in millivolts
        private static readonly double AnalogPinResolution = 1024;

        public double Temperature { get; private set; }

        public void ProcessFrame(IXBeeFrame frame)
        {
            IODataSample dataSample = frame as IODataSample;
            if (dataSample != null)
            {
                if (dataSample.Samples.Length == 2)
                {
                    // now read the temperature sensor reading
                    double analogReading = (dataSample.Samples[0] * 256 + dataSample.Samples[1]) * AnalogPinMaxVoltage / AnalogPinResolution;

                    Temperature = MosziNet.HomeAutomation.Sensor.Temperature.MCP9700.TemperatureFromVoltage(analogReading);
                }
                else
                {
                    Log.Debug("[TemperatureDeviceV1] Wrong number of samples received.");
                }
            }
            else
            {
                Log.Debug("[TemperatureDeviceV1] Wrong frame type (or null) for temperature sensor device.");
            }
        }

        public override DeviceState DeviceState
        {
            get
            {
                return new DeviceState()
                {
                    Device = this,
                    ComponentStateList = new ComponentState[]
                    {
                    new ComponentState() { Name = "MCP9700", Value = Temperature.ToString("N1") }
                    }
                };
            }
        }
    }
}
