using System;
using MosziNet.HomeAutomation.XBee;
using System.Threading;
using MosziNet.HomeAutomation.XBee.Frame;
using System.Collections;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.XBee
{
    public delegate void MessageReceivedDelegate(IXBeeFrame frame);

    /// <summary>
    /// Provides the means to send and receive XBee frames on an XBee network.
    /// </summary>
    public class XBeeService : IXBeeService
    {
        private XBeeSerialPort port;
        private ArrayList pendingMessages;

        public event MessageReceivedDelegate MessageReceived;

        public XBeeService()
        {
            pendingMessages = new ArrayList();

            // Migration
            port = null; // new XBeeSerialPort(SerialPorts.COM1, 2400, Parity.None, 8, StopBits.One);
        }

        /// <summary>
        /// Sends the frame to the XBee network.
        /// </summary>
        /// <param name="frame"></param>
        public void SendFrame(IXBeeFrame frame)
        {
            pendingMessages.Add(frame);
        }

        /// <summary>
        /// This method should be periodically invoked to receive and send messages.
        /// </summary>
        public void ProcessXBeeMessages()
        {
            try
            {
                CheckForXBeeMessages(port);

                SendAnyPendingXBeeMessages(port);
            }
            catch(Exception ex)
            {
                Log.Error("[XBeeService Exception] " + ExceptionFormatter.Format(ex));
            }
        }

        private void SendAnyPendingXBeeMessages(XBeeSerialPort port)
        {

            while (pendingMessages.Count > 0)
            {
                IXBeeFrame frame = (IXBeeFrame)pendingMessages[0];
                pendingMessages.RemoveAt(0);

                XBeeSerialPortWriter.WriteFrameToSerialPort(frame, port);
            }
        }

        private void CheckForXBeeMessages(XBeeSerialPort port)
        {
            // first try to read something
            IXBeeFrame frame = null;

            while ((frame = XBeeSerialPortReader.FrameFromSerialPort(port)) != null)
            {
                MessageReceivedDelegate e = this.MessageReceived;
                if (e != null)
                {
                    e(frame);
                }
            }
        }
    }
}
