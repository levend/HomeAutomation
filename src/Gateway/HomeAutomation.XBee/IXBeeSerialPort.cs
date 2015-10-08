using System;

namespace MosziNet.HomeAutomation.XBee
{
    /// <summary>
    /// Provides the means to read and write byte array frames to the XBee.
    /// </summary>
    public interface IXBeeSerialPort
    {
        /// <summary>
        /// Returns a frame from XBee based on it's frame protocol. (start, length, payload, checksum)
        /// </summary>
        /// <returns></returns>
        byte[] GetNextAvailableFrame();

        /// <summary>
        /// Writes a frame to the XBee.
        /// </summary>
        /// <param name="frame"></param>
        void WriteFrame(byte[] frame);
    }
}
