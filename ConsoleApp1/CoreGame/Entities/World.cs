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

        private readonly List<Worm> _worms = new();
        private readonly List<Food> _food = new();
        public int _gameIterationCounter;
        
        private IFoodGenerator _foodGenerator;
        private INameGenerator _nameGenerator;
        private IWormLogic _wormLogic;
        private ILogger _logger;
        
        private GameField _gameField = new();

        public World(
            IFoodGenerator foodGenerator,
            INameGenerator nameGenerator,
            IWormLogic wormLogic,
            ILogger logger
        ) 
        {
            _foodGenerator = foodGenerator;
            _nameGenerator = nameGenerator;
            _wormLogic = wormLogic;
            _logger = logger;
        }
        
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
            if (CheckCeil(food.CurrentPosition) == FieldObjects.Empty)
            {
                _food.Add(food);
            }
            else
            {
                foreach (Worm worm in _worms)
                {
                    if (worm.CurrentPosition == food.CurrentPosition)
                    {
                        worm.Health += 10;
                        return;
                    }
                }
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
        
        public void DecreaseHealths()
        {
            foreach (var worm in _worms)
            {
                worm.DecreaseHealth();
            }
            
            foreach (var food in _food)
            {
                food.DecreaseHealth();
            }
            
            _worms.RemoveAll(worm => worm.IsDeath);
            _food.RemoveAll(food => food.IsDeath);
        }

        public void MoveWorm(Worm worm, Directions direction)
        {
            var futurePosition = worm.GetAroundPosition(direction);
            
            if (CheckCeil(futurePosition) == FieldObjects.Worm)
            {
                throw new WormMovementException();
            }
            
            worm.Move(direction, this);
            
            foreach (var food in _food.ToArray())
            {
                if (food.CurrentPosition == futurePosition)
                {
                    _food.Remove(food);
                    worm.Health += 10;
                }
            }
        }

        public void BudWorm(Worm worm, Directions direction)
        {
            var futurePosition = worm.GetAroundPosition(direction);
            
            if (CheckCeil(futurePosition) != FieldObjects.Empty)
            {
                throw new WormBuddingException();
            }

            worm.Health -= 10;
            AddWorm(new Worm(futurePosition, _nameGenerator.Generate(), _wormLogic));
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