using System;

namespace HomeAutomation.Core
{
    /// <summary>
    /// Contains information about a device type participating in a <see cref="IDeviceNetwork"/>.
    /// </summary>
    public class DeviceTypeDescription
    {
        public int DeviceTypeId { get; private set; }

        public Type DeviceClassType { get; private set; }

        public DeviceTypeDescription(int deviceTypeId, Type deviceClassType)
        {
            this.DeviceTypeId = deviceTypeId;
            this.DeviceClassType = deviceClassType;
        }
    }
}
