namespace ConsoleApp1
{
    public interface IWormInfoProvider
    {
        public string ProvideName();
        public (int, int) ProvidePosition();
        public int ProvideHealth();
    }
}