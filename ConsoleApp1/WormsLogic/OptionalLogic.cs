using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.WormsLogic
{
    public class OptionalLogic: IWormLogic
    {
        public (Actions, Directions) Decide(IWormInfoProvider worm, IWorldInfoProvider infoProvider)
        {
            var action = Actions.None;
            var direction = Directions.None;
            
            var wormCoords = worm.ProvidePosition();

            if (worm.ProvideHealth() <= 30)
            {
                action = Actions.Move;
                var nearestFoodCoord = GetNearestFoodCoord(wormCoords, infoProvider);
                
                int deltaX = wormCoords.Item1 - nearestFoodCoord.Item1;
                int deltaY = wormCoords.Item2 - nearestFoodCoord.Item2;

                if (deltaX > 0)
                {
                    direction = Directions.Left;
                }
                if (deltaX < 0)
                {
                    direction = Directions.Right;
                }
                if (deltaY > 0)
                {
                    direction = Directions.Bottom;
                }
                if (deltaY < 0)
                {
                    direction = Directions.Top;
                }
            }
            else
            {
                action = Actions.Budding;
                
                var foodCoordList = infoProvider.ProvideFood().Select(food => food.ProvidePosition()).ToList();
                var wormsCoordList = infoProvider.ProvideWorms().Select(worms => worms.ProvidePosition()).ToList();

                if (!foodCoordList.Contains((wormCoords.Item1, wormCoords.Item2 + 1)) &&
                    !wormsCoordList.Contains((wormCoords.Item1, wormCoords.Item2 + 1)))
                {
                    direction = Directions.Top;
                }
                else if (!foodCoordList.Contains((wormCoords.Item1, wormCoords.Item2 - 1)) &&
                         !wormsCoordList.Contains((wormCoords.Item1, wormCoords.Item2 - 1)))
                {
                    direction = Directions.Bottom;
                }
                else if (!foodCoordList.Contains((wormCoords.Item1 + 1, wormCoords.Item2)) &&
                         !wormsCoordList.Contains((wormCoords.Item1 + 1, wormCoords.Item2)))
                {
                    direction = Directions.Right;
                }
                else if (!foodCoordList.Contains((wormCoords.Item1 - 1, wormCoords.Item2)) &&
                         !wormsCoordList.Contains((wormCoords.Item1 - 1, wormCoords.Item2)))
                {
                    direction = Directions.Left;
                }
            }
            
            return (action, direction);
        }
        
        private static (int, int) GetNearestFoodCoord((int, int) wormCoords, IWorldInfoProvider infoProvider)
        {
            int minLength = 10000;
            (int, int) nearestFoodCoord = (0, 0);
            
            foreach (var food in infoProvider.ProvideFood())
            {
                int deltaX = Math.Abs(wormCoords.Item1 - food.ProvidePosition().Item1);
                int deltaY = Math.Abs(wormCoords.Item2 - food.ProvidePosition().Item2);
                int totalDelta = deltaX + deltaY;
                    
                if (totalDelta < minLength)
                {
                    minLength = totalDelta;
                    nearestFoodCoord = food.ProvidePosition();
                }
            }

            return nearestFoodCoord;
        }
    }
}