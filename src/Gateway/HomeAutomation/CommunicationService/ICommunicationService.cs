using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.CommunicationService
{
    /// <summary>
    /// Provides the basis for every communication service.
    /// </summary>
    public interface ICommunicationService
    {
        /// <summary>
        /// Sends a message through the specific communication channel.
        /// </summary>
        void SendMessage(string destinationId, string message);

        /// <summary>
        /// The communication service will start listening for messages.
        /// </summary>
        /// <returns></returns>
        void StartListeningForMessages();
    }
}
