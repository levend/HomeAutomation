using System;
using Microsoft.SPOT;
using System.Collections;
using System.Reflection;

namespace MosziNet.HomeAutomation.XBee.Frame.ZigBee
{
    public enum PropertyType
    {
        Ignored,

        ByteArray,
        Byte,
        Integer,
    }

    /// <summary>
    /// Property Descriptor for a frame value. eg. Address, Frame Type
    /// </summary>
    public class PropertyDescriptor
    {
        public byte ByteCount;
        public string PropertyName;
        public PropertyType PropertyType;

        public PropertyDescriptor(byte byteCount, string propertyName, PropertyType propertyType)
        {
            ByteCount = byteCount;
            PropertyName = propertyName;
            PropertyType = propertyType;
        }

        public PropertyDescriptor(byte byteCount)
        {
            ByteCount = byteCount;
        }
    }

    public static class APIv1Descriptor
    {
        private static Hashtable descriptors;

        public static PropertyDescriptor[] GetFrameDescriptor(string frameName)
        {
            if (descriptors == null)
            {
                descriptors = new Hashtable();

                descriptors.Add("RemoteATCommand",
                     new PropertyDescriptor[] {
                        new PropertyDescriptor(1, "FrameStart", PropertyType.Ignored),
                        new PropertyDescriptor(2, "Length", PropertyType.Integer),
                        new PropertyDescriptor(1, "FrameType", PropertyType.Byte),
                        new PropertyDescriptor(1, "FrameId", PropertyType.Byte),
                        new PropertyDescriptor(8, "Address", PropertyType.ByteArray),
                        new PropertyDescriptor(2, "NetworkAddress", PropertyType.ByteArray),
                        new PropertyDescriptor(1, "CommandOptions", PropertyType.Byte),
                        new PropertyDescriptor(2, "ATCommand", PropertyType.ByteArray),
                        new PropertyDescriptor(0, "Parameters", PropertyType.ByteArray)
                });

                descriptors.Add("RemoteCommandResponse",
                    new PropertyDescriptor[] {
                        new PropertyDescriptor(1, "FrameStart", PropertyType.Ignored),
                        new PropertyDescriptor(2, "Length", PropertyType.Integer),
                        new PropertyDescriptor(1, "FrameType", PropertyType.Byte),
                        new PropertyDescriptor(1, "FrameId", PropertyType.Byte),
                        new PropertyDescriptor(8, "Address", PropertyType.ByteArray),
                        new PropertyDescriptor(2, "NetworkAddress", PropertyType.ByteArray),
                        new PropertyDescriptor(2, "ATCommand", PropertyType.ByteArray),
                        new PropertyDescriptor(1, "Status", PropertyType.Byte),
                        new PropertyDescriptor(4, "Parameters", PropertyType.ByteArray)
                });

                descriptors.Add("IODatasample",
                    new PropertyDescriptor[] {
                        new PropertyDescriptor(1, "FrameStart", PropertyType.Ignored),
                        new PropertyDescriptor(2, "Length", PropertyType.Integer),
                        new PropertyDescriptor(1, "FrameType", PropertyType.Byte),
                        new PropertyDescriptor(8, "Address", PropertyType.ByteArray),
                        new PropertyDescriptor(2, "NetworkAddress", PropertyType.ByteArray),

                        new PropertyDescriptor(1, "ReceiveOptions", PropertyType.Byte),
                        new PropertyDescriptor(1, "SampleCount", PropertyType.Byte),
                        new PropertyDescriptor(2, "DigitalMask", PropertyType.Integer),
                        new PropertyDescriptor(1, "AnalogMask", PropertyType.Byte),

                        new PropertyDescriptor(0, "Samples", PropertyType.Byte)
                });            
            }

            return descriptors.Contains(frameName) ? (PropertyDescriptor[])descriptors[frameName] : null;
        }
    }
}
