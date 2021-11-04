using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Generators
{
    public static class FoodCoordGenerator
    {
        private static readonly Random _random = new(DateTime.Now.Second);
        
        /*
         * функция генерации координат, которые гарантированно не совпадают с коордитатами другой еды на игровом поле
         */
        public static (int, int) GenerateFoodCoord(IWorldInfoProvider infoProvider)
        {
            var coordForResult = NextNormalPair();

            var foodList = infoProvider.ProvideFood();
            var foodCoords = foodList.Select(food => food.ProvidePosition()).ToList();

            while (foodCoords.Contains(coordForResult))
            {
                coordForResult = NextNormalPair();
            }
            
            return coordForResult;
        }
        
        public static (int, int) GenerateFoodCoord(List<(int, int)> foodCoords)
        {
            var foodLifetime = GameContract.StartFoodHealth / GameContract.HealthDecreasePerIteration;

            if (foodCoords.Count >= foodLifetime)
                return GetCoordsForLifetimePeriod(foodCoords, foodCoords.Count, foodLifetime);
            
            var coordForResult = NextNormalPair();

            while (foodCoords.Contains(coordForResult))
            {
                coordForResult = NextNormalPair();
            }
            
            return coordForResult;

        }

        private static (int, int) GetCoordsForLifetimePeriod(List<(int, int)> foodCoords, int index, int foodLifetime)
        {
            var actualRange = foodCoords.GetRange(index - foodLifetime, foodLifetime);

            var coordForResult = NextNormalPair();
            
            while (actualRange.Contains(coordForResult))
            {
                coordForResult = NextNormalPair();
            }

            return coordForResult;
        }

        /*
         * функция взятия пары случайных координат из нормального распределения; генерируются независимо друг от друга
         */
        private static (int, int) NextNormalPair()
        {
            return (
                NextNormal(_random, GameContract.Mu, GameContract.Sigma),
                NextNormal(_random, GameContract.Mu, GameContract.Sigma)
            );
        }
        
        /*
         * функция генерации числа из нормального распределения в с параметрами соответствии с заданием
         */
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