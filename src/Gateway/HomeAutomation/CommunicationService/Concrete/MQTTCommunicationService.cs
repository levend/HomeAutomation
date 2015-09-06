using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.CommunicationService
{
    public class MQTTCommunicationService : ICommunicationService
    {
        public void SendMessage(string destinationId, string message)
        {
            
        }

        private void MQTTMessageReceived(string topicId, string message)
        {

        }
    }
}
