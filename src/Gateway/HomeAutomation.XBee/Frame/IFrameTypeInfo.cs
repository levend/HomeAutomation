using System;
using System.Collections.Generic;

namespace MosziNet.HomeAutomation.XBee.Frame
{
    internal abstract class IFrameTypeInfo
    {
        public abstract Dictionary<FrameType, PropertyDescriptor[]> FrameTypeDescriptors { get; }

        public abstract Dictionary<FrameType, Type> FrameTypes { get; }

        /// <summary>
        /// Returns the descriptor list for the specified frame type, or null in case the frame type is not known
        /// </summary>
        /// <param name="frameType"></param>
        /// <returns></returns>
        public PropertyDescriptor[] GetFrameDescriptor(FrameType frameType)
        {
            Dictionary<FrameType, PropertyDescriptor[]> descriptors = FrameTypeDescriptors;

            return descriptors.ContainsKey(frameType) ? descriptors[frameType] : null;
        }
    }
}
