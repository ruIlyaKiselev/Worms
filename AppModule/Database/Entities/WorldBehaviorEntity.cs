using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.Database.Entities
{
    public class WorldBehaviorEntity
    {
        [Key]
        public string Name { get; set; }
        public string FoodCoords { get; set; }

        public WorldBehaviorEntity(string name, string foodCoords)
        {
            Name = name;
            FoodCoords = foodCoords;
        }
    }
}