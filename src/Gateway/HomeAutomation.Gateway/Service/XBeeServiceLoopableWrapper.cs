using System;
using MosziNet.HomeAutomation.XBee;

namespace MosziNet.HomeAutomation.Gateway.Service
{
    /// <summary>
    /// Provides a wrapper around the XBeeService which can participate in a runloop.
    /// </summary>
    public class XBeeServiceLoopableWrapper : IRunLoopParticipant
    {
        private IXBeeService xbeeService;

        public XBeeServiceLoopableWrapper(IXBeeService xbeeService)
        {
            this.xbeeService = xbeeService;
        }

        public void Execute()
        {
            xbeeService.ProcessXBeeMessages();
        }
    }
}
