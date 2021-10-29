using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Exceptions;
using ConsoleApp1.Generators;
using ConsoleApp1.Logging;
using ConsoleApp1.WormsLogic;

namespace ConsoleApp1
{
    public class World: IWorldInfoProvider
    {
        public int id = 1;
        public string name = "first";

        private List<Worm> _worms = new();
        private List<Food> _food = new();
        private int _gameIterationCounter;

        private IFoodGenerator _foodGenerator;
        private INameGenerator _nameGenerator;
        private IWormLogic _wormLogic;
        private ILogger _logger;
        
        private GameField _gameField = new();

        public List<Worm> GetWorms()
        {
            return _worms;
        } 
        
        public List<Food> GetFood()
        {
            return _food;
        }

        public void AddWorm(Worm worm)
        {
            if (CheckCeil(worm.CurrentPosition) == FieldObjects.Empty)
            {
                _worms.Add(worm);
            }
            else
            {
                throw new AddingWormException();
            }
        }

        public void AddFood(Food food)
        {
            foreach (Worm worm in _worms)
            {
                if (worm.CurrentPosition == food.CurrentPosition)
                {
                    worm.Health += 10;
                    return;
                }
            }

            if (CheckCeil(food.CurrentPosition) == FieldObjects.Empty)
            {
                _food.Add(food);
            }
        }

        public FieldObjects CheckCeil((int, int) ceilCoords)
        {
            var foodCoordList = _food.Select(food => food.ProvidePosition()).ToList();
            var wormsCoordList = _worms.Select(worms => worms.ProvidePosition()).ToList();

            if (wormsCoordList.Contains(ceilCoords))
            {
                return FieldObjects.Worm;
            }
            
            if (foodCoordList.Contains(ceilCoords))
            {
                return FieldObjects.Food;
            }

            return FieldObjects.Empty;
        }
        
        public List<IWormInfoProvider> ProvideWorms()
        {
            return _worms.Cast<IWormInfoProvider>().ToList();
        }

        public List<IFoodInfoProvider> ProvideFood()
        {
            return _food.Cast<IFoodInfoProvider>().ToList();
        }

        public GameField ProvideGameField()
        {
            return (GameField)_gameField.Clone();
        }

        public int ProvideGameIteration()
        {
            return _gameIterationCounter;
        }
    }
}