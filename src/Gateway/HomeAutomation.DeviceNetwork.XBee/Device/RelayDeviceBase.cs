using HomeAutomation.Core;
using HomeAutomation.Logging;
using HomeAutomation.Util;
using MosziNet.XBee;
using MosziNet.XBee.Frame;
using MosziNet.XBee.Frame.ZigBee;
using System;
using System.Collections.Generic;

namespace HomeAutomation.DeviceNetwork.XBee.Device
{
    /// <summary>
    /// An abstract class that defines the relay basics.
    /// </summary>
    public abstract class RelayDeviceBase : DeviceBase, IXBeeDevice
    {
        private const byte StateOFF = 0;
        private const byte StateON = 1;

        private const byte PinLow = 4;
        private const byte PinHigh = 5;

        private List<byte[]> xbeeConfiguration = new List<byte[]>();
        protected byte[] switchStates;
   
        public RelayDeviceBase(byte[] relay1XBeePin)
        {
            switchStates = new byte[1];
            xbeeConfiguration.Add(relay1XBeePin);
        }

        public RelayDeviceBase(byte[] relay1XBeePin, byte[] relay2XBeePin)
        {
            switchStates = new byte[2];
            xbeeConfiguration.Add(relay1XBeePin);
            xbeeConfiguration.Add(relay2XBeePin);
        }

        #region / DeviceBase implementation /

        /// <summary>
        /// Processes any frames sent to this device.
        /// </summary>
        /// <param name="frame"></param>
        public virtual void ProcessFrame(IXBeeFrame frame)
        {
            IODataSample dataSample = frame as IODataSample;
            if (dataSample != null)
            {
                // if there are digital pin states in the sample then use them for populating wht switchStates variable.
                if (dataSample.DigitalMask > 0 && dataSample.Samples.Length >= 2)
                {
                    int digitalReadingMLB = dataSample.Samples[0] * 256 + dataSample.Samples[1];

                    // build the switch states
                    for (int i = 0; i < switchStates.Length; i++)
                    {
                        switchStates[i] = (digitalReadingMLB & (2 ^ (i + 1))) != 0 ? StateOFF : StateON;
                    }
                }
            }
        }

        /// <summary>
        /// Builds the state of the switches.
        /// </summary>
        /// <returns></returns>
        public override DeviceState DeviceState
        {
            get
            {
                ComponentState[] states = new ComponentState[switchStates.Length];

                for (int i = 0; i < switchStates.Length; i++)
                {
                    states[i] = new ComponentState()
                    {
                        Name = $"Relay{i}",
                        Value = switchStates[i].ToString()
                    };
                }

                DeviceState state = new DeviceState()
                {
                    Device = this,
                    ComponentStateList = states
                };

                return state;
            }
        }

        #endregion / DeviceBase implementation /

        /// <summary>
        /// This is a method that will be invoked by commands sent to the system.
        /// </summary>
        /// <param name="relayIndex"></param>
        /// <param name="state"></param>
        public void SetRelayState(ushort relayIndex, byte state)
        {
            if (relayIndex >= xbeeConfiguration.Count)
                relayIndex = (byte)(xbeeConfiguration.Count - 1);

            if (state > StateON)
                state = StateON;

            switchStates[relayIndex] = state;

            IXBeeService xbeeService = ((XBeeDeviceNetwork)DeviceNetwork).XBeeService;

            // The relay is on the D1 and D2 pins, set the appropriate state for them
            xbeeService.SendFrame(XBeeFrameBuilder.CreateRemoteATCommand(
                (byte[])xbeeConfiguration[relayIndex],
                0,
                this.DeviceID,
                this.NetworkAddress,
                new byte[] { ((byte)(state == StateON ? PinLow : PinHigh)) },
                RemoteATCommand.OptionCommitChanges));

            Log.Debug($"[{DeviceID.ToHexString()}] Setting relay {relayIndex} to {state}");
        }
    }
}
