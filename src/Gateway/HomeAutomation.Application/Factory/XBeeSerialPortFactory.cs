﻿using HomeAutomation.Application.Configuration;
using HomeAutomation.NetCore.RPI;
using MosziNet.HomeAutomation.XBee;

namespace HomeAutomation.Application.Factory
{
    internal class XBeeSerialPortFactory : IXBeeSerialPortFactory
    {
        public IXBeeSerialPort Create(XBeeConfiguration config)
        {
            return new XBeeSerialPort(config.BaudRate, config.SerialParity, config.SerialStopBitCount, config.DataBits);
        }
    }
}
