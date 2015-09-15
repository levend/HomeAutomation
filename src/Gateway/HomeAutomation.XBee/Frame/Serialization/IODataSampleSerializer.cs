using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee.Frame.Serialization
{
    public class IODataSampleSerializer : BaseFrameSerializer
    {
        private static readonly double AnalogPinMaxVoltage = 1200.0; // in millivolts
        private static readonly double AnalogPinResolution = 1024;

        public override int Serialize(IXBeeFrame frame, byte[] resultArray, int offset)
        {
            return base.Serialize(frame, resultArray, offset);
        }

        public override void Deserialize(IXBeeFrame frame, byte[] buffer)
        {
            base.Deserialize(frame, buffer);

            IODataSampleFrame typedFrame = (IODataSampleFrame)frame;

            // now read the temperature sensor reading
            double analogReading = (buffer[19] * 256 + buffer[20]) * AnalogPinMaxVoltage / AnalogPinResolution;

            double[] analogReadings = new double[1];
            analogReadings[0] = analogReading;

            typedFrame.AnalogReadings = analogReadings;
        }

        public override FrameType FrameType
        {
            get { return Frame.FrameType.IODataSample; }
        }
    }
}
