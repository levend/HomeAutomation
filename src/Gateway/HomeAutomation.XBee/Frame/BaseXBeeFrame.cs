using System;
using Microsoft.SPOT;
using System.IO.Ports;

namespace MosziNet.HomeAutomation.XBee.Frame
{
    /// <summary>
    /// Represents a frame received from XBee.
    /// </summary>
    public abstract class BaseXBeeFrame : IXBeeFrame, IXbeeFrameSerializable
    {
        private FrameType frameType;

        public FrameType FrameType
        {
            get
            {
                return frameType;
            }

            internal set
            {
                frameType = value;
            }
        }

        public byte[] Address { get; set; }

        #region / IXbeeFrameSerializable interface implementation /

        public virtual void Deserialize(byte[] buffer)
        {
            // read the 64 bit hw address
            byte[] address = new byte[8];
            Array.Copy(buffer, 1, address, 0, 8);
            this.Address = address;
        }

        public virtual byte[] Serialize()
        {
            return new byte[0];
        }

        #endregion / IXbeeFrameSerializable interface implementation /
    }
}
