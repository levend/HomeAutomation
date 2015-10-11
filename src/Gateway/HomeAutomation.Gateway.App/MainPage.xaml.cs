using System;
using System.Collections.Generic;
using HomeAutomation.Application;
using HomeAutomation.Core;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HomeAutomation.Gateway.App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, IHomeController
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        public event EventHandler<DeviceCommand> DeviceCommandArrived;

        public void ExecuteTasks()
        {
            
        }

        public void SendDeviceState(DeviceState deviceState)
        {
            
        }

        public void SendGatewayHeartbeatMessage(string message)
        {
            
        }

        public void SendStatistics(Dictionary<string, object> statisticValues)
        {
            receivedXBeeFrameCount.Text = statisticValues["XBeeMessageReceiveCount"].ToString();
        }

        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MainApplication.Initialize("Config/HomeAutomation.conf");

            HomeAutomationSystem.ControllerRegistry.RegisterController(this);

            MainApplication.Run();
        }
    }
}
