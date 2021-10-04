using System;
using ConsoleApp1.WormsLogic;

namespace ConsoleApp1
{
    public class Worm: IWormInfoProvider
    {
        private IWormLogic _wormLogic;

        public Worm((int, int) currentPosition, string name)
        {
            Health = 10;
            ActionsIntent = Actions.None;
            DirectionIntent = Directions.None;
            CurrentPosition = currentPosition;
            Name = name;
            _wormLogic = new OptionalLogic();
        }

        public int Health { get; set; }
        public Actions ActionsIntent { get; set; } 
        public Directions DirectionIntent { get; set; }
        public (int, int) CurrentPosition { get; set; }
        public string Name { get; set; }

        public (Actions, Directions) GetIntent(IWorldInfoProvider infoProvider)
        {
            var decidedIntent = _wormLogic.Decide(this, infoProvider);
            ActionsIntent = decidedIntent.Item1;
            DirectionIntent = decidedIntent.Item2;
            
            return (ActionsIntent, DirectionIntent);
        }

        public bool IsDeath { get; set; }
        
        public bool DecreaseHealth()
        {
            Health -= 1;
            if (Health <= 0)
            {
                IsDeath = true;
            }

            return IsDeath;
        }

        public string ProvideName()
        {
            return Name;
        }

        public (int, int) ProvidePosition()
        {
            return CurrentPosition;
        }

        public int ProvideHealth()
        {
            return Health;
        }
    }
}