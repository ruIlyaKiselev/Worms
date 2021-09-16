using System;

namespace ConsoleApp1.WormsLogic
{
    public class FoodFindLogic: IWormLogic
    {
        public void Decide(Worm worm, GameContract.FieldObjects[,] gameField)
        {
            LiAlgorithm(worm.CurrentPosition, gameField);
        }
        
        private void LiAlgorithm((int, int) startCoord, GameContract.FieldObjects[,] gameField)
        {
            int[,] distanceField = new int[GameContract.Width, GameContract.Height];
            
            for (int i = 0; i != GameContract.Width; i++)
            {
                for (int j = 0; j != GameContract.Height; j++)
                {
                    distanceField[i, j] = -1;
                }
            }

            var translatedCoords = GameField.TranslateCoord(startCoord);
            distanceField[translatedCoords.Item1, translatedCoords.Item2] = 0;
            
            for (int i = 1; i != GameContract.Width - 1; i++)
            {
                for (int j = 1; j != GameContract.Height - 1; j++)
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
            
            
            for (int i = 0; i != GameContract.Width; i++)
            {
                for (int j = 0; j != GameContract.Width; j++)
                {
                    if (distanceField[i, j] == -1)
                    {
                        Console.Write("# ");
                    }
                    else
                    {
                        Console.Write(distanceField[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}