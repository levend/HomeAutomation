using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.XBee.Frame;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using System;
using System.Collections.Generic;

namespace MosziNet.HomeAutomation.XBee
{
    public delegate void MessageReceivedDelegate(IXBeeFrame frame);

    /// <summary>
    /// Provides the means to send and receive XBee frames on an XBee network.
    /// </summary>
    public class XBeeService : IXBeeService 
    {
        private IXBeeSerialPort port;
        private List<IXBeeFrame> pendingMessages = new List<IXBeeFrame>();

        public event MessageReceivedDelegate MessageReceived;

        public XBeeService(IXBeeSerialPort serialPort)
        {
            port = serialPort;
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
                CheckForXBeeMessages();

                SendAnyPendingXBeeMessages();
            }
            catch(Exception ex)
            {
                Log.Error("[XBeeService Exception] " + ExceptionFormatter.Format(ex));
            }
        }

        private void SendAnyPendingXBeeMessages()
        {
            while (pendingMessages.Count > 0)
            {
                IXBeeFrame frame = pendingMessages[0];
                pendingMessages.RemoveAt(0);

                WriteFrameToSerialPort(frame);
            }
        }

        private void CheckForXBeeMessages()
        {
            IXBeeFrame frame = null;

            // first try to read something
            while ((frame = FrameFromSerialPort()) != null)
            {
                this.MessageReceived?.Invoke(frame);
            }
        }

        private void WriteFrameToSerialPort(IXBeeFrame frame)
        {
            byte[] bytesToWrite = FrameSerializer.Serialize(frame);

            port.WriteFrame(bytesToWrite);

            // statistics counting
            XBeeStatistics.MessagesSent++;

            Log.Debug("[XBeeSerialPortWriter] Frame sent: " + HexConverter.ToSpacedHexString(bytesToWrite));
        }

        private IXBeeFrame FrameFromSerialPort()
        {
            IXBeeFrame frame = null;

            byte[] frameBytes = port.GetNextAvailableFrame();
            if (frameBytes != null)
            {
                // now create an XBee frame based on the buffer
                frame = FrameSerializer.Deserialize(frameBytes);

                // Log the frame ...
                Log.Debug("[XBeeSerialPortReader] Frame received: " + HexConverter.ToSpacedHexString(frameBytes, 0, frameBytes.Length));

                // statistics counting
                XBeeStatistics.MessagesReceived++;
            }

            return frame;
        }
    }
}
