using System;
using Microsoft.SPOT;
using MosziNet.HomeAutomation.XBee.Frame.Serialization;

namespace MosziNet.HomeAutomation.XBee.Frame
{
    /// <summary>
    /// Builds frames based on various parameters.
    /// </summary>
    public static class FrameFactory
    {
        /// <summary>
        /// Creates a frame based on the specified frame type.
        /// </summary>
        /// <param name="frameType"></param>
        /// <returns></returns>
        public static IXBeeFrame CreateFrameWithType(FrameType frameType)
        {
            IXBeeFrame frame = null;

            // Todo: replace this with a dictionary
            switch (frameType)
            {
                case FrameType.IODataSample:
                    frame = new IODataSampleFrame();
                    break;
                case FrameType.RemoteATCommand:
                    frame = new RemoteATCommandFrame();
                    break;
                case FrameType.RemoteCommandResponse:
                    frame = new RemoteCommandResponse();
                    break;
            }

            if (frame != null)
            {
                ((BaseXBeeFrame)frame).FrameType = (FrameType)frameType;
            }

            return frame;
        }

        public static IXbeeFrameSerializer CreateFrameSerializerWithType(FrameType frameType)
        {
            IXbeeFrameSerializer frameSerializer = null;

            switch (frameType)
            {
                case FrameType.IODataSample:
                    frameSerializer = new IODataSampleSerializer();
                    break;
                case FrameType.RemoteATCommand:
                    frameSerializer = new RemoteATCommandSerializer();
                    break;
                case FrameType.RemoteCommandResponse:
                    frameSerializer = new RemoteATCommandResponseSerializer();
                    break;
            }

            return frameSerializer;
        }
    }
}
