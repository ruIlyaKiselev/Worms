using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Generators;
using ConsoleApp1.Logging;

namespace ConsoleApp1
{
    public class GameController: IWorldInfoProvider
    {
        private List<Worm> _worms = new();
        private List<Food> _food = new();
        private GameField _gameField = new();
        private Logger _logger;
        private int _gameIterationCounter;

        public GameController()
        {
            _logger = new Logger(this);
        }
        
        public void GameProcess()
        {
            AddWorm((0, 0));
            AddFood();
            AddFood();
            AddFood();
            AddFood();
            AddFood();
            
            for (_gameIterationCounter = 0; _gameIterationCounter != GameContract.NumberOfSteps; _gameIterationCounter++)
            {
                _gameField.UpdateField(_worms.AsReadOnly(), _food.AsReadOnly());
                DecideWormsIntents();
                DecreaseHealths();
                AddFood();
                _logger.LogNewEvent();
            }
        }
        
        private void AddFood()
        {
            _food.Add(new Food(FoodCoordGenerator.GenerateFoodCoord(this)));
        }

        private void DecreaseHealths()
        {
            foreach (var food in _food.Where(food => food.DecreaseHealth()))
            {
                _food.Remove(food);
            }

            foreach (var worm in _worms.Where(worm => worm.DecreaseHealth()))
            {
                _worms.Remove(worm);
            }
        }

        private void DecideWormsIntents()
        {
            foreach (var worm in _worms)
            {
                var wormX = worm.CurrentPosition.Item1;
                var wormY = worm.CurrentPosition.Item2;
                var (wormAction, wormDirection) = worm.GetIntent(this);

                if (wormAction == Actions.Move)
                {
                    switch (wormDirection)
                    {
                        case Directions.Top:
                            if (_gameField.CheckCeil((wormX, wormY + 1)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX, wormY + 1);
                            } break;
                        case Directions.Bottom:
                            if (_gameField.CheckCeil((wormX, wormY - 1)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX, wormY - 1);
                            } break;
                        case Directions.Right:
                            if (_gameField.CheckCeil((wormX + 1, wormY)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX + 1, wormY);
                            } break;
                        case Directions.Left:
                            if (_gameField.CheckCeil((wormX - 1, wormY)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX - 1, wormY);
                            } break;
                    }
                }

                if (CheckFoodEat((wormX, wormY)))
                {
                    worm.Health += 10;
                }
            }
        }

        private bool CheckFoodEat((int, int) wormPosition)
        {
            foreach (var food in _food)
            {
                if (food.CurrentPosition.Item1 == wormPosition.Item1 &&
                    food.CurrentPosition.Item2 == wormPosition.Item2)
                {
                    _food.Remove(food);
                    return true;
                }
            }

            return false;
        }
        
        private void AddWorm((int, int) startCoord)
        {
            _worms.Add(new Worm(startCoord, "name"));
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