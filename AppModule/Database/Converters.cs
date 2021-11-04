using System;
using System.Collections.Generic;
using ConsoleApp1.Database.Entities;
using Newtonsoft.Json;

namespace ConsoleApp1.Database
{
    public static class Converters
    {
        public static string convertListToJson<T>(List<T> listToConvert)
        {
            return JsonConvert.SerializeObject(listToConvert, Formatting.Indented);
        }
        
        public static List<T> convertJsonToList<T>(string jsonToConvert)
        {
            return JsonConvert.DeserializeObject<List<T>>(jsonToConvert);
        }

        public static WorldBehaviorEntity ToEntity(this WorldBehavior worldBehavior)
        {
            return new WorldBehaviorEntity(worldBehavior.Name, convertListToJson(worldBehavior.FoodCoords));
        }
        
        public static WorldBehavior ToDomain(this WorldBehaviorEntity worldBehavior)
        {
            return new WorldBehavior(worldBehavior.Name, convertJsonToList<(int, int)>(worldBehavior.FoodCoords));
        }
    }
}