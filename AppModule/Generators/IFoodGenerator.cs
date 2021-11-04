namespace ConsoleApp1.Generators
{
    public interface IFoodGenerator
    {
        public Food GenerateFood(IWorldInfoProvider worldInfoProvider);
    }
}