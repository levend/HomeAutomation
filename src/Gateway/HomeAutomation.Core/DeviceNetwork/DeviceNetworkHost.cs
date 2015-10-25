namespace HomeAutomation.Core.DeviceNetwork
{
    public class DeviceNetworkHost
    {
        internal void SendDeviceNetworkDiagnostics(IDeviceNetwork deviceNetwork, object diagnostics)
        {
            HomeAutomationSystem.ControllerHost.SendDeviceNetworkDiagnostics(deviceNetwork, diagnostics);
        }
    }
}
