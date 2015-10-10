using HomeAutomation.Application;
using Windows.ApplicationModel.Background;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace HomeAutomation.Gateway.Service
{
    public sealed class StartupTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            MainApplication.Start("Config/HomeAutomation.conf");
        }
    }
}
