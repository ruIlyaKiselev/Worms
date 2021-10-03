using System;

namespace ConsoleApp1.WormsLogic
{
    public class FoodFindLogic: IWormLogic
    {
        public void Decide(Worm worm, GameField gameField)
        {
            var (coordX, coordY) = LiAlgorithm(worm.CurrentPosition, gameField);

            if (worm.Health <= 50)
            {
                worm.ActionsIntent = Actions.Move;
                var diffX = coordX - worm.CurrentPosition.Item1;
                var diffY = coordY - worm.CurrentPosition.Item2;

                if (diffX != 0)
                {
                    if (diffX > 0)
                    {
                        worm.DirectionIntent = Directions.Right;
                    }
                    if (diffX < 0)
                    {
                        worm.DirectionIntent = Directions.Left;
                    }
                }
                
                if (diffY != 0)
                {
                    if (diffY > 0)
                    {
                        worm.DirectionIntent = Directions.Top;
                    }
                    if (diffY < 0)
                    {
                        worm.DirectionIntent = Directions.Bottom;
                    }
                }
            }
            else
            {
                worm.ActionsIntent = Actions.Budding;
                worm.DirectionIntent = Directions.Top;
            }
        }
        
        private (int, int) LiAlgorithm((int, int) startCoord, GameField gameField)
        {
            int[,] distanceField = new int[GameContract.Width, GameContract.Height];
            
            for (int i = 0; i != GameContract.Width; i++)
            {
                for (int j = 0; j != GameContract.Height; j++)
                {
                    distanceField[i, j] = -1;
                }
            }

            var translatedCoords = FieldConverter.ToZeroBased(startCoord);
            distanceField[translatedCoords.Item1, translatedCoords.Item2] = 0;


            int biggestCoord = GameContract.Height > GameContract.Width ? GameContract.Height : GameContract.Width;
            
            for (int k = 0; k != biggestCoord - 1; k++)
            {
                CalculateDistanceFromTo(
                    distanceField, 
                    (1, 1), 
                    (GameContract.Width - 1, GameContract.Height - 1)
                );
            }

            int minRangeToFood = 100;
            (int, int) minCoord = (0, 0);
            foreach (var gameFieldFood in gameField.Foods)
            {
                var currentFoodPosition = FieldConverter.ToZeroBased(gameFieldFood.CurrentPosition);
                if (distanceField[currentFoodPosition.Item1, currentFoodPosition.Item2] < minRangeToFood)
                {
                    minCoord = currentFoodPosition;
                    minRangeToFood = distanceField[currentFoodPosition.Item1, currentFoodPosition.Item2];
                }
            }
            
            return FieldConverter.FromZeroBased(minCoord);
        }

        private void CalculateDistanceFromTo(int[,] distanceField, (int, int) startCoord, (int, int) finishCoord)
        {
            for (int i = startCoord.Item1; i != finishCoord.Item1; i++)
            {
                for (int j = startCoord.Item2; j != finishCoord.Item2; j++)
                {
                    if (distanceField[i, j] != -1)
                    {
                        if (distanceField[i + 1, j] == -1)
                        {
                            distanceField[i + 1, j] = distanceField[i, j] + 1;
                        } 
            
                        if (distanceField[i - 1, j] == -1)
                        {
                            distanceField[i - 1, j] = distanceField[i, j] + 1;
                        } 
            
                        if (distanceField[i, j + 1] == -1)
                        {
                            distanceField[i, j + 1] = distanceField[i, j] + 1;
                        } 
            
                        if (distanceField[i, j - 1] == -1)
                        {
                            distanceField[i, j - 1] = distanceField[i, j] + 1;
                        }
                    }
                }
            }
        }
    }
}