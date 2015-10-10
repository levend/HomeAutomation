using Windows.Devices.SerialCommunication;

namespace HomeAutomation.Application.Configuration
{
    public class XBeeConfiguration
    {
        public uint BaudRate { get; set; }

        public SerialParity SerialParity { get; set; }

        public SerialStopBitCount SerialStopBitCount { get; set; }

        public ushort DataBits { get; set; }
    }
}
