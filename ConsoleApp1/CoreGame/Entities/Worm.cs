using System;
using ConsoleApp1.WormsLogic;

namespace ConsoleApp1
{
    public class Worm: IWormInfoProvider
    {
        private int _health;
        private bool _isDeath;
        private Directions _directionsIntent = Directions.None;
        private Actions _actionsIntent = Actions.None;
        private IWormLogic _wormLogic;
        private (int, int) _currentPosition;
        private string _name;

        public Worm((int, int) currentPosition, string name)
        {
            _health = 10;
            _currentPosition = currentPosition;
            _name = name;
            _wormLogic = new FoodFindLogic();
        }
        
        public int Health { get; set; }
        public Actions ActionsIntent { get; set; }
        public Directions DirectionIntent { get; set; }
        public (int, int) CurrentPosition { get; set; }

        public string Name { get; set; }

        public int GetLengthToFood(IWorldInfoProvider infoProvider)
        {
            int minLength = 10000;
            foreach (var food in infoProvider.ProvideFood())
            {
                int deltaX = Math.Abs(_currentPosition.Item1 - food.ProvidePosition().Item1);
                int deltaY = Math.Abs(_currentPosition.Item2 - food.ProvidePosition().Item2);
                int totalDelta = deltaX + deltaY;
                
                if (totalDelta < minLength)
                {
                    minLength = totalDelta;
                }

                return minLength;
            }

            return 0;
        }
        
        public (Actions, Directions) GetIntent(IWorldInfoProvider infoProvider)
        {
            _wormLogic.Decide(this, infoProvider.ProvideGameField());
            return (_actionsIntent, _directionsIntent);
        }

        public bool IsDeath { get; set; }
        
        public bool DecreaseHealth()
        {
            _health -= 1;
            if (_health <= 0)
            {
                _isDeath = true;
            }

            return _isDeath;
        }

        public string ProvideName()
        {
            return _name;
        }

        public (int, int) ProvidePosition()
        {
            return _currentPosition;
        }

        public int ProvideHealth()
        {
            return _health;
        }
    }
}