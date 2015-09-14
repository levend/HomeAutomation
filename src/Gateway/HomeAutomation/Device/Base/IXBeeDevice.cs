using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.Device.Base;
using MosziNet.HomeAutomation.XBee.Frame;

namespace MosziNet.HomeAutomation.Device
{
    public interface IXBeeDevice : IDevice
    {
        void ProcessFrame(IXBeeFrame frame);
    }
}
