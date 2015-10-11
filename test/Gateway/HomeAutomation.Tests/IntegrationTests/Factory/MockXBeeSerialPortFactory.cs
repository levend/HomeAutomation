using System;
using HomeAutomation.Application.Configuration;
using HomeAutomation.Application.Factory;
using MosziNet.HomeAutomation.XBee;

namespace HomeAutomation.Tests.IntegrationTests.Factory
{
    class MockXBeeSerialPortFactory : IXBeeSerialPortFactory
    {
        public IXBeeSerialPort Create(XBeeConfiguration config)
        {
            return new MockXBeeSerialPort();
        }
    }
}
