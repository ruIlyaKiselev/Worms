using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using ConsoleApp1.Generators;

namespace ConsoleApp1
{
    public class GameController
    {
        private List<Worm> _worms = new List<Worm>();
        private List<Food> _food = new List<Food>();
        private GameField _gameField = new GameField();
        RandomName randomName = new RandomName(new Random(DateTime.Now.Second));
        
        
        public void GameProcess()
        {
            AddWorm();
            for (int i = 0; i != GameContract.NumberOfSteps; i++)
            {
                AddFood();
                _gameField.UpdateField(_worms.AsReadOnly(), _food.AsReadOnly());
                DecideWormsIntents();
                _gameField.UpdateField(_worms.AsReadOnly(), _food.AsReadOnly());
                _gameField.PrintField();
            }
        }
        
        private void AddFood()
        {
            _food.Add(new Food(FoodCoordGenerator.GenerateFoodCoord(_gameField)));
        }

        private void DecideWormsIntents()
        {
            var gameField = _gameField.GetFieldAsArray();
            foreach (var worm in _worms)
            {
                Console.WriteLine(worm.CurrentPosition.ToString());
                var wormIntent = worm.GetIntent(gameField);

                if (wormIntent.Item1 == Actions.Move)
                {
                    switch (wormIntent.Item2)
                    {
                        case Directions.Top:
                            if (_gameField.CheckCeil((worm.CurrentPosition.Item1, worm.CurrentPosition.Item2 + 1)) !=
                                GameContract.FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (worm.CurrentPosition.Item1, worm.CurrentPosition.Item2 + 1);
                            } break;
                        case Directions.Bottom:
                            if (_gameField.CheckCeil((worm.CurrentPosition.Item1, worm.CurrentPosition.Item2 - 1)) !=
                                GameContract.FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (worm.CurrentPosition.Item1, worm.CurrentPosition.Item2 - 1);
                            } break;
                        case Directions.Right:
                            if (_gameField.CheckCeil((worm.CurrentPosition.Item1 + 1, worm.CurrentPosition.Item2)) !=
                                GameContract.FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (worm.CurrentPosition.Item1 + 1, worm.CurrentPosition.Item2);
                            } break;
                        case Directions.Left:
                            if (_gameField.CheckCeil((worm.CurrentPosition.Item1 - 1, worm.CurrentPosition.Item2)) !=
                                GameContract.FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (worm.CurrentPosition.Item1 - 1, worm.CurrentPosition.Item2);
                            } break;
                    }
                }
            }
        }

        private bool CheckEmptyCeil()
        {
            return false;
        }

        private bool ValidateCoordInBounds()
        {
            return false;
        }
        private void AddWorm()
        {
            string name = randomName.Generate(Sex.Male);
            _worms.Add(new Worm((0, 0), name));
        }
    }
}