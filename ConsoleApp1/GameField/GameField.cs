using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ConsoleApp1
{
    public class GameField: ICheckCeil
    {
        private FieldObjects[,] _field = new FieldObjects[GameContract.Width, GameContract.Height];
        private List<Food> _food = new List<Food>();
        private List<Worm> _worms = new List<Worm>();
        
        public void UpdateField(ReadOnlyCollection<Worm> worms, ReadOnlyCollection<Food> foods)
        {
            for (int i = 0; i != GameContract.Width; i++)
            {
                for (int j = 0; j != GameContract.Width; j++)
                {
                    _field[i, j] = FieldObjects.Empty;
                }
            }
            
            foreach (var food in foods)
            {
                (int, int ) translatedCoords = FieldConverter.ToZeroBased(food.CurrentPosition);
                _field[translatedCoords.Item1, translatedCoords.Item2] = FieldObjects.Food;
                _food.Add(food);
            }
            
            foreach (var worm in worms)
            {
                (int, int ) translatedCoords = FieldConverter.ToZeroBased(worm.CurrentPosition);
                _field[translatedCoords.Item1, translatedCoords.Item2] = FieldObjects.Worm;
                _worms.Add(worm);
            }
        }

        public FieldObjects CheckCeil((int, int) coord)
        {
            (int, int) translatedCoords = FieldConverter.ToZeroBased(coord);
            // Console.WriteLine(coord + "(" + FoodCoordGenerator.ValidateCoordInBounds(coord) + ")" + " ->  " + translatedCoords);
            return _field[translatedCoords.Item1, translatedCoords.Item2];
        }

        public void PrintField()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i != GameContract.Width; i++)
            {
                for (int j = 0; j != GameContract.Height; j++)
                {
                    stringBuilder.Append(FieldConverter.FieldObjectConverter(_field[j, i]) + " ");
                }

                stringBuilder.Append('\n');
            }
            Console.WriteLine(stringBuilder.ToString());
        }

        public FieldObjects[,] GetFieldAsArray()
        {
            return _field.Clone() as FieldObjects[,];
        }

        public List<Food> Foods => _food;
        public List<Worm> Worms => _worms;
    }
}