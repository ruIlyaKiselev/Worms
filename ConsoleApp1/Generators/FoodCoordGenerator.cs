using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConsoleApp1.Generators
{
    public static class FoodCoordGenerator
    {
        private static readonly Random _random = new Random(DateTime.Now.Second);
        
        public static (int, int) GenerateFoodCoord(ICheckCeil gameField)
        {
            var coordForResult = NextCoordinate();
            
            while (gameField.CheckCeil((coordForResult.Item1, coordForResult.Item2)) == GameContract.FieldObjects.Food)
            {
                coordForResult = NextCoordinate();
            }
            
            return coordForResult;
        }
        
        private static (int, int) NextCoordinate()
        {
            var pair = NextNormalPair();

            while (!ValidateCoordInBounds(pair))
            {
                pair = NextNormalPair();
            }
            
            return pair;
        }
        
        public static bool ValidateCoordInBounds((int, int) coords)
        {
            return (coords.Item1 >= GameContract.StartX && coords.Item1 <= GameContract.FinishX) 
                   && (coords.Item2 >= GameContract.StartY && coords.Item2 <= GameContract.FinishY);
        }
        
        private static (int, int) NextNormalPair()
        {
            return (
                NextNormal(_random, GameContract.Mu, GameContract.Sigma),
                NextNormal(_random, GameContract.Mu, GameContract.Sigma)
            );
        }
        
        private static int NextNormal(Random r, double mu = 0, double sigma = 1)
        {
            var u1 = r.NextDouble();
            var u2 = r.NextDouble();

            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            var randNormal = mu + sigma * randStdNormal;

            return (int)Math.Round(randNormal);
        }
    }
}