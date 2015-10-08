﻿using MosziNet.HomeAutomation.XBee.Frame.ZigBee;
using System;
using System.Collections.Generic;

namespace HomeAutomation.DeviceNetwork.XBee.FrameProcessor
{
    internal static class XBeeFrameProcessorFactory
    {
        static Dictionary<Type, IXBeeFrameProcessor> processorDictionary = new Dictionary<Type, IXBeeFrameProcessor>();

        static XBeeFrameProcessorFactory()
        {
            // register xbee frame processors
            processorDictionary.Add(typeof(RemoteCommandResponse), new RemoteCommandResponseProcessor());
            processorDictionary.Add(typeof(IODataSample), new IODataSampleFrameProcessor());
        }

        public static IXBeeFrameProcessor GetProcessorByFrameType(Type frameType)
        {
            return processorDictionary.ContainsKey(frameType) ? processorDictionary[frameType] : null;
        }
    }
}
