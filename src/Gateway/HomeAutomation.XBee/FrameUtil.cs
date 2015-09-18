using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.XBee
{
    public static class FrameUtil
    {
        /// <summary>
        /// Returns the length stored in the frame.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static int FrameContentLength(byte[] buffer)
        {
            return buffer[FrameIndex.LengthMSB] * 256 + buffer[FrameIndex.LengthLSB];
        }

        /// <summary>
        /// Returns the length stored in the frame, plus the additional bytes at the beginning and at the end of the frame.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static int FrameTotalLength(byte[] buffer)
        {
            return FrameContentLength(buffer) + 4; // +1 FrameStart, +2 FrameLength, +1 Checksum
        }

        /// <summary>
        /// Returns wheter the frame is valid by verifying it's checksum.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static bool IsValidFrame(byte[] buffer)
        {
            int checksumIndex = CalculateChecksumIndex(buffer);
            byte storedChecksum = buffer[checksumIndex];

            return storedChecksum == CalculateChecksum(buffer);
        }

        /// <summary>
        /// Returns the index of the checksum byte in the frame.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static int CalculateChecksumIndex(byte[] buffer)
        {
            int contentLength = FrameContentLength(buffer);

            return FrameIndex.FrameType + contentLength;
        }

        /// <summary>
        /// Returns the checksum of the frame.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte CalculateChecksum(byte[] buffer)
        {
            int checksum = 0;
            int contentLength = FrameContentLength(buffer);

            for (int i = FrameIndex.FrameType; i < contentLength + FrameIndex.FrameType; i++)
            {
                checksum += buffer[i];
            }

            return (byte)(0xFF - (checksum & 0xFF));
        }

        public static bool IsSameATCommand(byte[] atCommand1, byte[] atCommand2)
        {
            return (atCommand1[0] == atCommand2[0]) && (atCommand1[1] == atCommand2[1]);
        }

    }
}
