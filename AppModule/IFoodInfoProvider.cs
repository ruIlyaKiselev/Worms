namespace ConsoleApp1
{
    public interface IFoodInfoProvider
    {
        public (int, int) ProvidePosition();
        public int ProvideHealth();
    }
}