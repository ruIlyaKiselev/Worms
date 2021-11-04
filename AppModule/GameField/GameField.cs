using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ConsoleApp1
{
    public class GameField: ICheckCeil, ICloneable
    {
        private FieldObjects[,] _field = new FieldObjects[GameFieldContract.Width, GameFieldContract.Height];
        private List<Food> _food = new();
        private List<Worm> _worms = new();
        
        public void UpdateField(ReadOnlyCollection<Worm> worms, ReadOnlyCollection<Food> foods)
        {
            for (int i = 0; i != GameFieldContract.Width; i++)
            {
                for (int j = 0; j != GameFieldContract.Width; j++)
                {
                    _field[i, j] = FieldObjects.Empty;
                }
            }
            
            foreach (var food in foods)
            {
                var (coordX, coordY) = FieldConverter.ToZeroBased(food.CurrentPosition);
                _field[coordX, coordY] = FieldObjects.Food;
                _food.Add(food);
            }
            
            foreach (var worm in worms)
            {
                var (coordX, coordY) = FieldConverter.ToZeroBased(worm.CurrentPosition);
                _field[coordX, coordY] = FieldObjects.Worm;
                _worms.Add(worm);
            }
        }

        public FieldObjects CheckCeil((int, int) coord)
        {
            var (coordX, coordY) = FieldConverter.ToZeroBased(coord);
            return _field[coordX, coordY];
        }

        public void PrintField()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i != GameFieldContract.Width; i++)
            {
                for (int j = 0; j != GameFieldContract.Height; j++)
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

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}