using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device.Base;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;

namespace MosziNet.HomeAutomation.Device.Concrete
{
    public class TemperatureSensor : DeviceBase
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

                    Temperature = HomeAutomation.Sensor.Temperature.MCP9700.TemperatureFromVoltage(analogReading);

                    Debug.Print("[TemperatureSensor] Temperature: " + Temperature.ToString());
                }
                else
                {
                    Debug.Print("[TemperatureSensor] Wrong number of samples received.");
                }
            }
            else
            {
                Debug.Print("[TemperatureSensor] Wrong frame type (or null) for temperature sensor device.");
            }
        }
    }
}
