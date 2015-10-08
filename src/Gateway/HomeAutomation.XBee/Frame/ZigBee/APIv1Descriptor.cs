using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace MosziNet.HomeAutomation.XBee.Frame.ZigBee
{
    internal class APIv1Descriptor : IFrameTypeInfo
    {
        private Dictionary<FrameType, PropertyDescriptor[]> descriptors;
        private Dictionary<FrameType, Type> wellKnownTypes;

        #region / IFrameTypeInfo interface /

        public override Dictionary<FrameType, PropertyDescriptor[]> FrameTypeDescriptors
        {
            get
            {
                return descriptors;
            }
        }

        public override Dictionary<FrameType, Type> FrameTypes
        {
            get
            {
                return wellKnownTypes;
            }
        }

        #endregion / IFrameTypeInfo interface /

        public APIv1Descriptor()
        {
            SetupDescriptors();
            SetupWellKnownTypes();
        }

        private void SetupWellKnownTypes()
        {
            wellKnownTypes = new Dictionary<FrameType, Type>()
            {
                [FrameType.RemoteATCommand] = typeof(RemoteATCommand),
                [FrameType.RemoteCommandResponse] = typeof(RemoteCommandResponse),
                [FrameType.IODataSample] = typeof(IODataSample)
            };
        }

        private void SetupDescriptors()
        {
            descriptors = new Dictionary<FrameType, PropertyDescriptor[]>();

            descriptors.Add(FrameType.RemoteATCommand,
                 new PropertyDescriptor[] {
                        new PropertyDescriptor(1, "FrameStart", PropertyType.Ignored),

                        new PropertyDescriptor(2, nameof(RemoteATCommand.Length), PropertyType.Integer),
                        new PropertyDescriptor(1, nameof(RemoteATCommand.FrameType), PropertyType.Byte),
                        new PropertyDescriptor(1, nameof(RemoteATCommand.FrameId), PropertyType.Byte),
                        new PropertyDescriptor(8, nameof(RemoteATCommand.Address), PropertyType.ByteArray),
                        new PropertyDescriptor(2, nameof(RemoteATCommand.NetworkAddress), PropertyType.ByteArray),

                        new PropertyDescriptor(1, nameof(RemoteATCommand.CommandOptions), PropertyType.Byte),
                        new PropertyDescriptor(2, nameof(RemoteATCommand.ATCommand), PropertyType.ByteArray),
                        new PropertyDescriptor(0, nameof(RemoteATCommand.Parameters), PropertyType.ByteArray)
            });

            descriptors.Add(FrameType.RemoteCommandResponse,
                new PropertyDescriptor[] {
                        new PropertyDescriptor(1, "FrameStart", PropertyType.Ignored),

                        new PropertyDescriptor(2, nameof(RemoteCommandResponse.Length), PropertyType.Integer),
                        new PropertyDescriptor(1, nameof(RemoteCommandResponse.FrameType), PropertyType.Byte),
                        new PropertyDescriptor(1, nameof(RemoteCommandResponse.FrameId), PropertyType.Byte),
                        new PropertyDescriptor(8, nameof(RemoteCommandResponse.Address), PropertyType.ByteArray),
                        new PropertyDescriptor(2, nameof(RemoteCommandResponse.NetworkAddress), PropertyType.ByteArray),

                        //
                        new PropertyDescriptor(2, nameof(RemoteCommandResponse.ATCommand), PropertyType.ByteArray),
                        new PropertyDescriptor(1, nameof(RemoteCommandResponse.Status), PropertyType.Byte),
                        new PropertyDescriptor(4, nameof(RemoteCommandResponse.Parameters), PropertyType.ByteArray)
            });

            descriptors.Add(FrameType.IODataSample,
                new PropertyDescriptor[] {
                        new PropertyDescriptor(1, "FrameStart", PropertyType.Ignored),

                        new PropertyDescriptor(2, nameof(IODataSample.Length), PropertyType.Integer),
                        new PropertyDescriptor(1, nameof(IODataSample.FrameType), PropertyType.Byte),
                        new PropertyDescriptor(8, nameof(IODataSample.Address), PropertyType.ByteArray),
                        new PropertyDescriptor(2, nameof(IODataSample.NetworkAddress), PropertyType.ByteArray),

                        //
                        new PropertyDescriptor(1, nameof(IODataSample.ReceiveOptions), PropertyType.Byte),
                        new PropertyDescriptor(1, nameof(IODataSample.SampleCount), PropertyType.Byte),
                        new PropertyDescriptor(2, nameof(IODataSample.DigitalMask), PropertyType.Integer),
                        new PropertyDescriptor(1, nameof(IODataSample.AnalogMask), PropertyType.Byte),
                        //
                        new PropertyDescriptor(0, nameof(IODataSample.Samples), PropertyType.ByteArray)
            });
        }
    }
}
