using System;
using MosziNet.HomeAutomation.XBee;

namespace MosziNet.HomeAutomation
{
    public class XBeeServiceWrapper : IRunLoopParticipant
    {
        private IXBeeService xbeeService;

        public XBeeServiceWrapper(IXBeeService xbeeService)
        {
            this.xbeeService = xbeeService;
        }

        public void Execute()
        {
            xbeeService.ProcessXBeeMessages();
        }
    }
}
