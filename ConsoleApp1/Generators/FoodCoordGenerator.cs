using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Generators
{
    public static class FoodCoordGenerator
    {
        private static readonly Random _random = new(DateTime.Now.Second);
        
        /*
         * функция генерации координат, которые гарантированно не выходят за пределы массива-игрового поля
         * и не совпадают с коордитатами другой еды на игровом поле
         */
        public static (int, int) GenerateFoodCoord(ICheckCeil gameField)
        {
            var coordForResult = NextCoordinate();
            
            while (gameField.CheckCeil((coordForResult.Item1, coordForResult.Item2)) == FieldObjects.Food)
            {
                coordForResult = NextCoordinate();
            }
            
            return coordForResult;
        }
        
        public static (int, int) GenerateFoodCoord(IWorldInfoProvider infoProvider)
        {
            var coordForResult = NextCoordinate();

            var foodList = infoProvider.ProvideFood();
            var foodCoords = foodList.Select(food => food.ProvidePosition()).ToList();

            while (foodCoords.Contains(coordForResult))
            {
                coordForResult = NextCoordinate();
            }
            
            return coordForResult;
        }
        
        /*
         * функция генерации координат, которые гарантированно не выходят за пределы массива-игрового поля
         */
        private static (int, int) NextCoordinate()
        {
            var pair = NextNormalPair();

            while (!ValidateCoordInBounds(pair))
            {
                pair = NextNormalPair();
            }
            
            return pair;
        }
        
        /*
         * функция проверки сгенерированных координат на выход за пределы массива, который хранит информацию
         * об игровом поле
         */
        public static bool ValidateCoordInBounds((int, int) coords)
        {
            return (coords.Item1 >= GameContract.StartX && coords.Item1 <= GameContract.FinishX) 
                   && (coords.Item2 >= GameContract.StartY && coords.Item2 <= GameContract.FinishY);
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