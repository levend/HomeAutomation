using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee
{
    public static class XBeeFrameSerialization
    {
        private static readonly double AnalogPinMaxVoltage = 1200.0; // in millivolts
        private static readonly double AnalogPinResolution = 1024;

        public static XBeeFrame Deserialize(byte[] buffer, int length)
        {
            // minimum message length is 18 - TODO remove magic number
            if (length < 18)
            {
                return null;
            }

            XBeeFrame frame = new XBeeFrame();

            // now read the temperature sensor reading
            double analogReading = (buffer[16] * 256 + buffer[17]) * AnalogPinMaxVoltage / AnalogPinResolution;

            double[] analogReadings = new double[1];
            analogReadings[0] = analogReading;

            frame.AnalogReadings = analogReadings;

            return frame;
        }
    }
}
