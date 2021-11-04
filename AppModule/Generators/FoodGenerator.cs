namespace ConsoleApp1.Generators
{
    public class FoodGenerator: IFoodGenerator
    {
        public Food GenerateFood(IWorldInfoProvider worldInfoProvider)
        {
            return new Food(FoodCoordGenerator.GenerateFoodCoord(worldInfoProvider));
        }
    }
}