namespace ConsoleApp1
{
    public static class GameContract
    {
        public static readonly int NumberOfSteps = 100;
        
        public static readonly double Mu = 0;
        public static readonly double Sigma = 5;

        public static readonly int StartWormHealth = 10;
        public static readonly int StartFoodHealth = 10;
        public static readonly int FoodSaturation = 10;
        public static readonly int HealthDecreasePerIteration = 1;
    }
}