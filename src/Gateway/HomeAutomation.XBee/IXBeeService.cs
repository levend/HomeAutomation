using System;
namespace MosziNet.HomeAutomation.XBee
{
    public interface IXBeeService
    {
        event MessageReceivedDelegate MessageReceived;
        void ProcessXBeeMessages();
        void SendFrame(MosziNet.HomeAutomation.XBee.Frame.IXBeeFrame frame);
    }
}
