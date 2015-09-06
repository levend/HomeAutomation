using System;
using Microsoft.SPOT;
using System.IO.Ports;

namespace MosziNet.HomeAutomation.XBee
{
    /// <summary>
    /// Represents a frame received from XBee.
    /// </summary>
    public class XBeeFrame
    {
        public double[] AnalogReadings { get; set; }
    }
}
