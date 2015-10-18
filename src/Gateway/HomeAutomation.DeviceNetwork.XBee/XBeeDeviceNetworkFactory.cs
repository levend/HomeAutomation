using HomeAutomation.Core;
using HomeAutomation.Core.Network;
using MosziNet.XBee;
using System.Collections.Generic;
using Windows.Devices.SerialCommunication;

namespace HomeAutomation.DeviceNetwork.XBee
{
    class XBeeDeviceNetworkFactory : IDeviceNetworkFactory
    {
        public IDeviceNetwork CreateDeviceNetwork(Dictionary<string, object> configuration)
        {
            IXBeeSerialPort serialPort = new XBeeSerialPort(
                (uint)configuration["BaudRate"], 
                (SerialParity)configuration["SerialParity"], 
                (SerialStopBitCount)configuration["SerialStopBitCount"], 
                (ushort)configuration["DataBits"]);

            XBeeDeviceNetwork network = new XBeeDeviceNetwork(serialPort);

            return network;
        }
    }
}
