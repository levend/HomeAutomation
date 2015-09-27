using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device.Base;
using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.Device.Concrete
{
    public class DoubleRelayLM35 : DeviceBase
    {
        private static readonly double AnalogPinMaxVoltage = 1200.0; // in millivolts
        private static readonly double AnalogPinResolution = 1024;

        public double Temperature { get; private set; }
        public int Switch1State { get; private set; }
        public int Switch2State { get; private set; }

        public override void ProcessFrame(XBee.Frame.IXBeeFrame frame)
        {
            IODataSample dataSample = frame as IODataSample;
            if (dataSample != null)
            {
                if (dataSample.Samples.Length == 4)
                {
                    // now read the digital readings, and the temperature sensor reading
                    byte digitalReadingMLB = dataSample.Samples[1];

                    // build the switch states
                    Switch1State = (digitalReadingMLB & 0x04) != 0 ? 1 : 0;
                    Switch2State = (digitalReadingMLB & 0x02) != 0 ? 1 : 0;

                    double analogReading = (dataSample.Samples[2] * 256 + dataSample.Samples[3]) * AnalogPinMaxVoltage / AnalogPinResolution;

                    Temperature = HomeAutomation.Sensor.Temperature.LM35.TemperatureFromVoltage(analogReading);

                    Log.Debug("[TemperatureSensor] Temperature: " + Temperature.ToString());
                }
                else
                {
                    Log.Debug("[TemperatureSensor] Wrong number of samples received: " + HexConverter.ToSpacedHexString(dataSample.Samples));
                }
            }
            else
            {
                Log.Debug("[TemperatureSensor] Wrong frame type (or null) for temperature sensor device.");
            }
        }

        public void SetRelayState(string relayIndexString, string stateString)
        {
            byte relayIndex = Byte.Parse(relayIndexString);
            if (relayIndex > 1)
                relayIndex = 1;

            byte state = Byte.Parse(stateString);
            if (state > 1)
                state = 1;

            // build the frame to send to the device
            RemoteATCommand commandToSend = new XBeeFrameBuilder().CreateRemoteATCommand(
                relayIndex == 0 ? ATCommands.D1 : ATCommands.D2,
                0, // do not expect a response back
                this.DeviceID,
                this.NetworkAddress,
                new byte[] { state });

            // send the frame to the device
            XBeeService xbeeService = (XBeeService)ApplicationContext.ServiceRegistry.GetServiceOfType(typeof(XBeeService));
            xbeeService.SendFrame(commandToSend);

            Log.Debug("Setting relay " + relayIndexString + " to " + stateString);
        }

        public override DeviceState GetDeviceState()
        {
            return new DeviceState()
            {
                Device = this,
                ComponentStateList = new ComponentState[] 
                {
                    new ComponentState() { Name = "LM35", Value = Temperature.ToString("N1") },
                    new ComponentState() { Name = "Relay1", Value = Switch1State.ToString() },
                    new ComponentState() { Name = "Relay2", Value = Switch2State.ToString() }
                }
            };

        }
    }
}
