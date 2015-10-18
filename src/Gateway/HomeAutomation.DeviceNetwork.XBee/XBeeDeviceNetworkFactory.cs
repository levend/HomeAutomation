using HomeAutomation.Core;
using HomeAutomation.Core.Network;
using MosziNet.XBee;
using System;
using System.Collections.Generic;
using Windows.Devices.SerialCommunication;

namespace HomeAutomation.DeviceNetwork.XBee
{
    class XBeeDeviceNetworkFactory : IDeviceNetworkFactory
    {
        public IDeviceNetwork CreateDeviceNetwork(Dictionary<string, string> configuration)
        {
            uint baudRate = UInt32.Parse(configuration["BaudRate"]);
            SerialParity parity = (SerialParity)Enum.Parse(typeof(SerialParity), configuration["SerialParity"], true);
            SerialStopBitCount stopBit = (SerialStopBitCount)Enum.Parse(typeof(SerialStopBitCount), configuration["SerialStopBitCount"], true);
            ushort dataBits = UInt16.Parse(configuration["DataBits"]);

            IXBeeSerialPort serialPort = new XBeeSerialPort(baudRate, parity, stopBit, dataBits);

            XBeeDeviceNetwork network = new XBeeDeviceNetwork(serialPort);

            return network;
        }
    }
}
