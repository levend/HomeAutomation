using System;
using HomeAutomation.Application.Configuration;
using HomeAutomation.Application.Factory;
using MosziNet.HomeAutomation.XBee;

namespace HomeAutomation.Tests.IntegrationTests.Factory
{
    class MockXBeeSerialPortFactory : IXBeeSerialPortFactory
    {
        static IXBeeSerialPort serialPort = new MockXBeeSerialPort();

        public static MockXBeeSerialPortFactory Instance { get; private set; } = new MockXBeeSerialPortFactory();

        public IXBeeSerialPort Create(XBeeConfiguration config)
        {
            return serialPort;
        }

        public IXBeeSerialPort CreateNew()
        {
            return new MockXBeeSerialPort();
        }
    }
}
