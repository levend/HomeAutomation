namespace HomeAutomation.Core
{
    /// <summary>
    /// Provides easy access to information related to device networks, device instances, devices types, etc.
    /// </summary>
    public static class HomeAutomationSystem
    {
        public static DeviceTypeRegistry DeviceTypeRegistry { get; private set; } = new DeviceTypeRegistry();

        public static DeviceRegistry DeviceRegistry { get; private set; } = new DeviceRegistry();

        public static DeviceNetworkRegistry DeviceNetworkRegistry { get; private set; } = new DeviceNetworkRegistry();

        public static ControllerRegistry ControllerRegistry { get; private set; } = new ControllerRegistry();
    }
}
