using HomeAutomation.Core.Controller;
using System;

namespace HomeAutomation.Core
{
    /// <summary>
    /// Provides an well known interface for the controllers to interact with the gateway.
    /// </summary>
    public class ControllerHost
    {
        public event EventHandler<ControllerDiagnosticsEventArgs> OnControllerDiagnosticsReceived;

        public event EventHandler<DeviceNetworkDiagnosticsEventArgs> OnDeviceNetworkDiagnosticsReceived;

        /// <summary>
        /// Sends diagnostics information to other controllers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="diagnostics"></param>
        public void SendControllerDiagnostics(IController sender, object diagnostics)
        {
            OnControllerDiagnosticsReceived?.Invoke(sender, new ControllerDiagnosticsEventArgs(diagnostics));
        }

        internal void SendDeviceNetworkDiagnostics(IDeviceNetwork deviceNetwork, object diagnostics)
        {
            OnDeviceNetworkDiagnosticsReceived?.Invoke(deviceNetwork, new DeviceNetworkDiagnosticsEventArgs(diagnostics));
        }
    }
}
