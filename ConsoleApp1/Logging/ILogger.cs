namespace ConsoleApp1.Logging
{
    public interface ILogger
    {
        public void LogNewEvent();
        public void LogNewEvent(IWorldInfoProvider infoProvider);
    }
}