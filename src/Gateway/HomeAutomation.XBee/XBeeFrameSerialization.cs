using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee
{
    public static class XBeeFrameSerialization
    {
        public static XBeeFrame Deserialize(byte[] buffer, int length)
        {
            // minimum message length is 18.
            if (length < 18)
            {
                return null;
            }

            XBeeFrame frame = new XBeeFrame();

            // now read the temperature sensor reading
            double analogReading = (buffer[16] * 256 + buffer[17]) * 1200.0 / 1024.0;

            double[] analogReadings = new double[1];
            analogReadings[0] = analogReading;

            frame.AnalogReadings = analogReadings;

            return frame;
        }
    }
}
