using System;
using System.Collections.ObjectModel;
using System.Text;
using ConsoleApp1.Generators;

namespace ConsoleApp1
{
    public class GameField: ICheckCeil
    {
        private GameContract.FieldObjects[,] _field = new GameContract.FieldObjects[GameContract.Width, GameContract.Height];
        
        public void UpdateField(ReadOnlyCollection<Worm> worms, ReadOnlyCollection<Food> foods)
        {
            for (int i = 0; i != GameContract.Width; i++)
            {
                for (int j = 0; j != GameContract.Width; j++)
                {
                    _field[i, j] = GameContract.FieldObjects.Empty;
                }
            }
            
            foreach (var food in foods)
            {
                (int, int ) translatedCoords = TranslateCoord(food.CurrentPosition);
                _field[translatedCoords.Item1, translatedCoords.Item2] = GameContract.FieldObjects.Food;
            }
            
            foreach (var worm in worms)
            {
                (int, int ) translatedCoords = TranslateCoord(worm.CurrentPosition);
                _field[translatedCoords.Item1, translatedCoords.Item2] = GameContract.FieldObjects.Worm;
            }
        }

        public GameContract.FieldObjects CheckCeil((int, int) coord)
        {
            (int, int) translatedCoords = TranslateCoord(coord);
            // Console.WriteLine(coord + "(" + FoodCoordGenerator.ValidateCoordInBounds(coord) + ")" + " ->  " + translatedCoords);
            return _field[translatedCoords.Item1, translatedCoords.Item2];
        }

        public static (int, int) TranslateCoord((int, int) coord)
        {
            return (coord.Item1 - GameContract.StartX, -coord.Item2 + GameContract.FinishY);
        }

        public void PrintField()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i != GameContract.Width; i++)
            {
                for (int j = 0; j != GameContract.Height; j++)
                {
                    stringBuilder.Append(GameContract.FieldObjectConverter(_field[j, i]) + " ");
                }

                stringBuilder.Append('\n');
            }
            Console.WriteLine(stringBuilder.ToString());
        }

        public GameContract.FieldObjects[,] GetFieldAsArray()
        {
            return _field.Clone() as GameContract.FieldObjects[,];
        }
    }
}