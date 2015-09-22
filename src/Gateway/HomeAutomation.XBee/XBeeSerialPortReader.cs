using System;
using Microsoft.SPOT;
using System.IO.Ports;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using System.Threading;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.XBee
{
    public static class XBeeSerialPortReader
    {
        private static byte[] readBuffer = new byte[XBeeConstants.MaxFrameLength];

        public static IXBeeFrame FrameFromSerialPort(ISerialPort port)
        {
            IXBeeFrame frame = null;

            // the serial port needs to have at least 3 bytes: 1 frame start, 2 bytes for frame length
            if (port.BytesToRead >= 3) 
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
                    int frameLength = readBuffer[FrameIndex.LengthMSB] * 256 + readBuffer[FrameIndex.LengthLSB] + 1; // NOTE +1 for checksum

                    if (frameLength <= XBeeConstants.MaxFrameLength - 4)
                    {
                        // we begin reading with the frame type offset, frameLength count of bytes (including checksum)
                        port.Read(readBuffer, FrameIndex.FrameType, frameLength); 

                        // now create an XBee frame based on the buffer
                        frame = FrameSerializer.Deserialize(readBuffer);

                        Log.Debug("[XBeeSerialPortReader] Frame received: " + HexConverter.ToSpacedHexString(readBuffer, 0, frameLength + 3));
                    }
                    else
                    {
                        // discard the frame it if it's too big.
                        DiscardBytes(port, frameLength);

                        Log.Debug("[XBeeSerialPortReader] Discaring frame with length: " + frameLength);
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
