using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee;

namespace MosziNet.HomeAutomation
{
    public class XBeeServiceWrapper : IRunLoopParticipant
    {
        private XBeeService xbeeService;

        public XBeeServiceWrapper(XBeeService xbeeService)
        {
            this.xbeeService = xbeeService;
        }

        public void Execute()
        {
            xbeeService.ProcessXBeeMessages();
        }
    }
}
