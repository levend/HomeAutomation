using MosziNet.HomeAutomation.XBee.Frame;
using System;
namespace MosziNet.HomeAutomation.XBee
{
    /// <summary>
    /// Interface to be used with XBeeService. Provides easy mocking possibility.
    /// </summary>
    public interface IXBeeService
    {
        /// <summary>
        /// Raised when a message is received from the XBee.
        /// </summary>
        event EventHandler<IXBeeFrame> MessageReceived;

        /// <summary>
        /// Processess all pending writes to XBee and reads frames if possible.
        /// </summary>
        void ProcessXBeeMessages();

        /// <summary>
        /// Sends a frame to the XBee.
        /// </summary>
        /// <param name="frame"></param>
        void SendFrame(MosziNet.HomeAutomation.XBee.Frame.IXBeeFrame frame);
    }
}
