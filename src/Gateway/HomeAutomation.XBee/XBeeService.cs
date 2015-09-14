using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee;
using System.IO.Ports;
using SecretLabs.NETMF.Hardware.Netduino;
using System.Threading;
using MosziNet.HomeAutomation.XBee.Frame;
using System.Collections;

namespace MosziNet.HomeAutomation.XBee
{
    public delegate void MessageReceivedDelegate(IXBeeFrame frame);

    /// <summary>
    /// Provides the means to send and receive XBee frames on an XBee network.
    /// </summary>
    public class XBeeService
    {
        private bool shouldListenForMessages;
        private ArrayList pendingMessages;

        public event MessageReceivedDelegate MessageReceived;

        public XBeeService()
        {
            pendingMessages = new ArrayList();

            StartListeningForMessages();
        }

        public void SendCommand(byte[] address, string command)
        {
            IXBeeFrame frame = FrameFactory.CreateFrameWithType(FrameType.RemoteATCommand);

            // todo: build the frame based on the parameters

            lock(pendingMessages.SyncRoot)
            {
                pendingMessages.Add(frame);
            }
        }

        public void StartListeningForMessages()
        {
            new Thread(XBeeMessageListenerThread).Start();
        }

        private void XBeeMessageListenerThread()
        {
            using (XBeeSerialPort port = new XBeeSerialPort(SerialPorts.COM1, 9600, Parity.None, 8, StopBits.One))
            {
                shouldListenForMessages = true;

                // Todo: when to stop listening for messages
                while (shouldListenForMessages)
                {
                    CheckForXBeeMessages(port);

                    SendAnyPendingXBeeMessages(port);

                    Thread.Sleep(100);
                }
            }
        }

        private void SendAnyPendingXBeeMessages(XBeeSerialPort port)
        {
            // first make a copy of the messages that have to be sent.
            ArrayList localList = null;
            lock(pendingMessages.SyncRoot)
            {
                localList = (ArrayList)pendingMessages.Clone();
                
                while (pendingMessages.Count > 0)
                    pendingMessages.RemoveAt(0);
            }

            // and now send out these messages
            for(int i = 0; i < localList.Count; i++)
            {
                // todo
            }
        }

        private void CheckForXBeeMessages(XBeeSerialPort port)
        {
            // first try to read something
            IXBeeFrame frame = XBeeSerialPortReader.FrameFromSerialPort(port);

            if (frame != null)
            {
                Debug.Print("Message received from: " + frame.Address);

                MessageReceivedDelegate e = this.MessageReceived;
                if (e != null)
                {
                    e(frame);
                }
            }
        }
    }
}
