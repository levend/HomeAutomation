namespace HomeAutomation.Core.Scheduler
{
    public interface IRunner
    {
        void Step();
        void Start();
        void Stop();
    }
}