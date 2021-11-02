using System;
using System.Collections.Generic;
using ConsoleApp1.Generators;

namespace ConsoleApp1
{
    public class WorldBehavior
    {
        public String Name { get; set; }
        public List<(int, int)> FoodCoords { get; set; }

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
        
        public WorldBehavior(string name, List<(int, int)> foodCoords)
        {
            Name = name;
            FoodCoords = foodCoords;
        }
    }
}