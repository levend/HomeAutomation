using System;
using Microsoft.SPOT;
using System.IO.Ports;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.XBee.Frame.Serialization;

namespace MosziNet.HomeAutomation.XBee
{
    public static class XBeeSerialPortReader
    {
        // we limit the max frame length to 200 bytes - anything above this size will be discarded.
        private const byte MaxFrameLength = 200;

        private static byte[] readBuffer = new byte[MaxFrameLength];

        public static IXBeeFrame FrameFromSerialPort(ISerialPort port)
        {
            IXBeeFrame frame = null;

            // the serial port needs to have at least 4 bytes: 1 frame start, 2 bytes for frame length, 1 byte for checksum

            if (port.BytesToRead >= 4)
            {
                // read the possible frame start into the buffer
                readBuffer[FrameIndex.Start] = (byte)port.ReadByte();

                // read on byte, see if it's a valid frame start byte
                if (readBuffer[FrameIndex.Start] == XBeeConstants.FrameStart)
                {
                    // read the length into the buffer
                    readBuffer[FrameIndex.LengthMSB] = (byte)port.ReadByte();
                    readBuffer[FrameIndex.LengthLSB] = (byte)port.ReadByte();

                    // now read the length of the frame
                    // +1 comes from the fact that the frame ends with a checksum byte
                    int frameLength = readBuffer[FrameIndex.LengthMSB] * 256 + readBuffer[FrameIndex.LengthLSB] + 1;

                    if (frameLength <= MaxFrameLength - 4)
                    {
                        // check if there is enough to read for the framelength
                        if (port.BytesToRead >= frameLength)
                        {
                            port.Read(readBuffer, FrameIndex.FrameType, frameLength); // first 3 bytes skipped (frame start, +2 bytes frame length)

                            // now create an XBee frame based on the buffer
                            frame = BaseFrameSerializer.Deserialize(readBuffer, frameLength);
                        }
                    }
                    else
                    {
                        // discard the frame it if it's too big.
                        DiscardBytes(port, frameLength);
                    }
                }
            }

            return frame;
        }

        private static void DiscardBytes(ISerialPort port, int count)
        {
            for (int i = 0; i < count; i++)
            {
                port.ReadByte();
            }
        }
    }
}
