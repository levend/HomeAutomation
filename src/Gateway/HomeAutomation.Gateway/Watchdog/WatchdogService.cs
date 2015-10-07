using System;
using System.Threading;
using MosziNet.HomeAutomation.Logging;
using MosziNet.HomeAutomation.Messaging;
using MosziNet.HomeAutomation.Mqtt;
using MosziNet.HomeAutomation.Util;

namespace MosziNet.HomeAutomation.Watchdog
{
    public class WatchdogService
    {
        private IMessageBus messageBus;
        private MqttService mqttService;
        private byte counter = 0;

        private class HeartbeatCommand : IProcessableMessage
        {
            public MqttService MqttService { get; set; }
            public string Message { get; set; }

            public void ProcessMessage()
            {
                MqttService.SendMessage(MqttService.GetFullTopicName(MqttTopic.Heartbeat), Message);
            }
        }

        public WatchdogService(IMessageBus messageBus, MqttService mqttService)
        {
            this.messageBus = messageBus;
            this.mqttService = mqttService;

            //new Thread(WatchdogThread).Start();
        }

        private void WatchdogThread()
        {
            while(true)
            {
                try
                {
                    messageBus.PostMessage(new HeartbeatCommand()
                    {
                        MqttService = mqttService,
                        Message = "WatchdogService I'm here. My counter is " + counter++
                    });

                    // Migration
                    //Thread.Sleep(10 * 1000);
                }
                catch (Exception ex)
                {
                    messageBus.PostMessage(new HeartbeatCommand()
                    {
                        MqttService = mqttService,
                        Message = "WatchdogService Exception " + ExceptionFormatter.Format(ex)
                    });
                }
            }
        }
    }
}
