using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.CoreGame;
using ConsoleApp1.CoreGame.Interfaces;

namespace ConsoleApp1.Generators
{
    /// <summary>
    ///     Утильный класс для генерации случайных уникальных координат для спавна еды
    /// </summary>
    public static class FoodCoordGenerator
    {
        private static readonly Random _random = new(DateTime.Now.Second);
        
        /// <summary>
        ///     Функция генерации координат, которые гарантированно не совпадают с коордитатами другой еды на игровом поле.
        /// </summary>
        /// <param name="infoProvider">
        ///     Интерфейс для получения необходимой информации о мире
        /// </param>
        /// <returns>
        ///     Возвращает пару (int, int), уникальную среди всех координат еды в мире.
        /// </returns>
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
        
        /// <summary>
        ///     Функция генерации координат, которые гарантированно не совпадают с коордитатами другой еды на игровом поле.
        /// </summary>
        /// <param name="foodCoords">
        ///     Набор координат еды в мире
        /// </param>
        /// <returns>
        ///     Возвращает пару (int, int), уникальную среди всех координат из переданного набора foodCoords.
        /// </returns>
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

        /// <summary>
        ///     Функция генерации координат, которые гарантированно не совпадают с
        ///     последними несколькими коордитатами другой еды на игровом поле.
        ///     Нужна для генерации набора координат для <c>WorldBehavior</c>, чтобы учесть случай,
        ///     когда еда на карте "портится" и на её месте может появиться другая еда.
        /// </summary>
        /// <param name="foodCoords">
        ///     Набор координат еды в мире
        /// </param>
        /// <param name="index">
        ///     Индекс из листа еды, начиная с которого стоит смотреть,
        ///     чтобы сгенерированные координаты не совпадали с координатами еды от index до конца листа.
        /// </param>
        /// <returns>
        ///     Возвращает пару (int, int), уникальную среди всех координат из переданного набора foodCoords
        ///     за последние foodLifetime шагов.
        /// </returns>
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

        /// <summary>
        ///     Функция взятия пары случайных координат из нормального распределения.
        ///     Генерируются независимо друг от друга.
        /// </summary>
        /// <returns>
        ///     Возвращает пару чисел нормального распределения (int, int)
        /// </returns>
        private static (int, int) NextNormalPair()
        {
            return (
                NextNormal(_random, GameContract.Mu, GameContract.Sigma),
                NextNormal(_random, GameContract.Mu, GameContract.Sigma)
            );
        }
        
        /// <summary>
        ///     Функция генерации числа из нормального распределения в с параметрами соответствии с правилами.
        /// <summary>
        /// /// <returns>
        ///     Возвращает число int нормального распределения
        /// </returns>
        private static int NextNormal(Random r, double mu, double sigma)
        {
            var u1 = r.NextDouble();
            var u2 = r.NextDouble();

            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            var randNormal = mu + sigma * randStdNormal;

            return (int)Math.Round(randNormal);
        }
    }
}