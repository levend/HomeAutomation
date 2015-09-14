using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee.Frame
{
    /// <summary>
    /// Contains readings from an XBee. Eg frame: 7E 00 12 92 00 13 A2 00 40 E4 38 E3 82 8F 01 01 00 00 01 02 6A F9
    /// </summary>
    public class IODataSampleFrame : BaseXBeeFrame
    {
        private static readonly double AnalogPinMaxVoltage = 1200.0; // in millivolts
        private static readonly double AnalogPinResolution = 1024;

        /// <summary>
        /// Data samples read by the xbee.
        /// </summary>
        public double[] AnalogReadings { get; set; }

        public override void Deserialize(byte[] buffer)
        {
            base.Deserialize(buffer);

            // now read the temperature sensor reading
            double analogReading = (buffer[16] * 256 + buffer[17]) * AnalogPinMaxVoltage / AnalogPinResolution;

            double[] analogReadings = new double[1];
            analogReadings[0] = analogReading;

            this.AnalogReadings = analogReadings;
        }
    }
}
