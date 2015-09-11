using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee;
using System.IO.Ports;
using SecretLabs.NETMF.Hardware.Netduino;
using System.Threading;
using MosziNet.HomeAutomation.XBee.Frame;

namespace MosziNet.HomeAutomation.CommunicationService
{
    public class XBeeCommunicationService : ICommunicationService
    {
        SerialPortWrapper portWrapper;
        bool shouldListenForMessages;

        public XBeeCommunicationService()
        {
            StartListeningForMessages();
        }

        public void SendMessage(string destinationId, string message)
        {
            
        }

        public void StartListeningForMessages()
        {
            new Thread(XBeeMessageListenerThread).Start();
        }

        private void XBeeMessageListenerThread()
        {
            portWrapper = new SerialPortWrapper(SerialPorts.COM1, 9600, Parity.None, 8, StopBits.One);
            shouldListenForMessages = true;

            XBeeSerialPortReader xbeeReader = new XBeeSerialPortReader();

            // Todo: when to stop listening for messages
            while(shouldListenForMessages)
            {
                IXBeeFrame frame = xbeeReader.FrameFromSerialPort(portWrapper);

                if (frame != null)
                {
                    MessageReceived(frame);
                }

                Thread.Sleep(10);
            }
        }


        private void MessageReceived(IXBeeFrame frame)
        {
            Debug.Print("Message received from: " + frame.Address);
        }
    }
}
