using System;
using System.Collections.Generic;
using ConsoleApp1.CoreGame.Interfaces;
using ConsoleApp1.Generators;

namespace ConsoleApp1.CoreGame.Domain
{
    /// <summary>
    ///     Класс <c>WorldBehavior</c> служит для сохранения поведения мира.
    ///     Имплементирует интерфейс <c>IFoodGenerator</c>,
    ///     то есть может служить генератором еды вместо рандомного генератора.
    ///     Имеет уникальное имя для сохранения в БД и лист координат еды для спавна.
    /// </summary>
    public class WorldBehavior: IFoodGenerator
    {
        /// <value>Property <c>Name</c> хранит в себе имя данного поведения мира.</value>
        public String Name { get; set; }
        /// <value>Property <c>FoodCoords</c> хранит в себе лист со всеми координатами для спавна еды.</value>
        public List<(int, int)> FoodCoords { get; set; }

        /// <summary>
        ///     Конструктор <c>WorldBehavior</c> принимает строку-имя поведения мира,
        ///     координаты для спавна еды генерируются рандомно.
        /// </summary>
        /// <param name="name">
        ///     string name - уникальное имя поведения мира.
        /// </param>
        public WorldBehavior(String name)
        {
            Name = name;
            FoodCoords = new List<(int, int)>();

            for (int i = 0; i != GameContract.NumberOfSteps; i++)
            {
                var newFoodCoords = FoodCoordGenerator.GenerateFoodCoord(FoodCoords);
                FoodCoords.Add(newFoodCoords);
            }
        }
        /// <summary>
        ///     Конструктор <c>WorldBehavior</c> принимает строку-имя поведения мира и лист с координатами для спавна еды.
        /// </summary>
        /// <param name="name">
        ///     Строка name - уникальное имя поведения мира.
        /// </param>
        /// <param name="foodCoords">
        ///     Лист координат foodCoords - лист с координатами, по которым еда будет спавниться.
        /// </param>
        public WorldBehavior(string name, List<(int, int)> foodCoords)
        {
            Name = name;
            FoodCoords = foodCoords;
        }

        public Food GenerateFood(IWorldInfoProvider worldInfoProvider)
        {
            return new Food(FoodCoords[worldInfoProvider.ProvideGameIteration()]);
        }
    }
}