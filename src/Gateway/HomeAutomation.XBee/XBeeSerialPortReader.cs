using System;
using Microsoft.SPOT;
using System.IO.Ports;
using MosziNet.HomeAutomation.XBee.Frame;

namespace MosziNet.HomeAutomation.XBee
{
    public class XBeeSerialPortReader
    {
        // we limit the max frame length to 200 bytes - anything above this size will be discarded
        private const byte MaxFrameLength = 200;

        private static byte[] readBuffer = new byte[MaxFrameLength];

        public IXBeeFrame FrameFromSerialPort(ISerialPort port)
        {
            IXBeeFrame frame = null;

            // the serial port needs to have at least 4 bytes: 1 frame start, 2 bytes for frame length, 1 byte for checksum

            if (port.BytesToRead >= 4)
            {
                // read on byte, see if it's a valid frame start byte
                if (port.ReadByte() == XBeeConstants.FrameStart)
                {
                    // now read the length of the frame
                    int frameLength = port.ReadByte() * 256 + port.ReadByte();

                    if (frameLength <= MaxFrameLength)
                    {
                        // check if there is enough to read for the framelength
                        if (port.BytesToRead >= frameLength)
                        {
                            port.Read(readBuffer, 0, frameLength);

                            // now create an XBee frame based on the buffer
                            frame = XBeeFrameSerialization.Deserialize(readBuffer, frameLength);
                        }
                    }
                    else
                    {
                        // discard the fram it if it's too big.
                        DiscardBytes(port, frameLength);
                    }

                    // finally read the checksum byte
                    port.ReadByte();
                }
            }

            return frame;
        }

        private void DiscardBytes(ISerialPort port, int count)
        {
            for (int i = 0; i < count; i++)
            {
                port.ReadByte();
            }
        }
    }
}
