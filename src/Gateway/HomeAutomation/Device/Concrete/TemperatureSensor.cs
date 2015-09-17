using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device.Base;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;

namespace MosziNet.HomeAutomation.Device.Concrete
{
    public class TemperatureSensor : DeviceBase, IXBeeDevice
    {
        public double Temperature { get; private set; }

        public void ProcessFrame(XBee.Frame.IXBeeFrame frame)
        {
            IODataSample dataSample = frame as IODataSample;
            if (dataSample != null)
            {
                Temperature = HomeAutomation.Sensor.Temperature.MCP9700.TemperatureFromVoltage(dataSample.AnalogReadings[0]);

                Debug.Print("Temperature: " + Temperature.ToString());
            }
            else
            {
                Debug.Print("Wrong frame type (or null) for temperature sensor device.");
            }
        }
    }
}
