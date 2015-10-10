using System;
using HomeAutomation.Core;
using MosziNet.HomeAutomation.XBee;
using MosziNet.HomeAutomation.XBee.Frame;
using HomeAutomation.DeviceNetwork.XBee.FrameProcessor;
using HomeAutomation.Logging;
using HomeAutomation.DeviceNetwork.XBee.Device;

namespace HomeAutomation.DeviceNetwork.XBee
{
    /// <summary>
    /// Defines the network that is powered by XBee devices.
    /// </summary>
    public class XBeeDeviceNetwork : IDeviceNetwork
    {
        public event EventHandler<DeviceState> DeviceStateReceived;

        private IXBeeService xbeeService;

        public XBeeDeviceNetwork(IXBeeSerialPort serialPort)
        {
            xbeeService = new XBeeService(serialPort);

            xbeeService.MessageReceived += XbeeService_MessageReceived;
        }

        /// <summary>
        /// Returns the known device types for this device network.
        /// </summary>
        public DeviceTypeDescription[] AvailableDeviceTypes
        {
            get
            {
                return new DeviceTypeDescription[]
                {
                    new DeviceTypeDescription(0x9999, typeof(FakeTemperatureDevice)),
                    new DeviceTypeDescription(0x9988, typeof(TemperatureDeviceV1)),
                    new DeviceTypeDescription(0x9986, typeof(TemperatureDeviceV2)),
                    new DeviceTypeDescription(0x9985, typeof(DoubleRelayLM35)),
                    new DeviceTypeDescription(0x9984, typeof(DoubleRelay))
                };
            }
        }

        /// <summary>
        /// Sends a command to the XBee network.
        /// </summary>
        /// <param name="command"></param>
        public void SendCommand(DeviceCommand command)
        {
            IXBeeDevice device = HomeAutomationSystem.DeviceRegistry.GetDeviceById(this, command.DeviceID) as IXBeeDevice;

            device?.ExecuteCommand(command);
        }

        /// <summary>
        /// A new device state is ready to be published, announce it
        /// </summary>
        /// <param name="deviceState"></param>
        internal void AnnounceDeviceState(DeviceState deviceState)
        {
            DeviceStateReceived?.Invoke(this, deviceState);
        }

        internal IXBeeService XBeeService
        {
            get
            {
                return xbeeService;
            }
        }

        private void XbeeService_MessageReceived(object sender, IXBeeFrame e)
        {
            Type frameType = e.GetType();

            IXBeeFrameProcessor processor = XBeeFrameProcessorFactory.GetProcessorByFrameType(frameType);

            if (processor != null)
            {
                processor.ProcessFrame(this, e);
            }
            else
            {
                Log.Debug("Dropping frame with type " + frameType.Name + " as no suitable processor is found.");
            }
        }

        public void ExecuteTasks()
        {
            xbeeService.ProcessXBeeMessages();
        }
    }
}
