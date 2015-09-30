using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using System.Collections;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Util;
using MosziNet.HomeAutomation.Device.Base;

namespace MosziNet.HomeAutomation.Device.Concrete
{
    /// <summary>
    /// An abstract class that defines the relay basics.
    /// </summary>
    public abstract class RelayDeviceBase : DeviceBase
    {
        private const byte StateOFF = 0;
        private const byte StateON = 0;

        private ArrayList xbeeConfiguration = new ArrayList();
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
        public override void ProcessFrame(XBee.Frame.IXBeeFrame frame)
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
        public override DeviceState GetDeviceState()
        {
            ComponentState[] states = new ComponentState[switchStates.Length];

            for(int i = 0; i < switchStates.Length; i++)
            {
                states[i] = new ComponentState()
                {
                    Name = "Relay" + i.ToString(),
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

        #endregion / DeviceBase implementation /

        /// <summary>
        /// This is a method that will be invoked by commands sent to the system.
        /// </summary>
        /// <param name="relayIndexString"></param>
        /// <param name="stateString"></param>
        public void SetRelayState(string relayIndexString, string stateString)
        {
            byte relayIndex = Byte.Parse(relayIndexString);
            if (relayIndex > 1)
                relayIndex = 1;

            if (relayIndex >= xbeeConfiguration.Count)
                relayIndex = (byte)(xbeeConfiguration.Count - 1);

            byte state = Byte.Parse(stateString);
            if (state > 1)
                state = 1;

            IXBeeService xbeeService = (IXBeeService)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(IXBeeService));

            // The relay is on the D1 and D2 pins, set the appropriate state for them
            xbeeService.SendFrame(new XBeeFrameBuilder().CreateRemoteATCommand(
                (byte[])xbeeConfiguration[relayIndex],
                0,
                this.DeviceID,
                this.NetworkAddress,
                new byte[] { ((byte)(state == 1 ? 4 : 5)) },
                RemoteATCommand.OptionCommitChanges));

            Log.Debug("[" + HexConverter.ToHexString(this.DeviceID) + "] Setting relay " + relayIndexString + " to " + stateString);
        }
    }
}
