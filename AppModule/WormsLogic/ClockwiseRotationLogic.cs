using System.Collections.Generic;
using ConsoleApp1.CoreGame.Enums;
using ConsoleApp1.CoreGame.Interfaces;

namespace ConsoleApp1.WormsLogic
{
    /// <summary>
    ///     Реализация интерфейса IWormLogic.
    ///     Является зацикленным перемещением по одним и тем же координатам в списке.
    /// </summary>
    public class ClockwiseRotationLogic: IWormLogic
    {
        private readonly List<Directions> _directionsList = new();
        private int _stepsCounter;
        
        public ClockwiseRotationLogic()
        {
            _stepsCounter = 0;

            _directionsList.Add(Directions.Top);
            _directionsList.Add(Directions.Right);
            _directionsList.Add(Directions.Bottom);
            _directionsList.Add(Directions.Bottom);
            _directionsList.Add(Directions.Left);
            _directionsList.Add(Directions.Left);
            _directionsList.Add(Directions.Top);
            _directionsList.Add(Directions.Top);
            _directionsList.Add(Directions.Right);
            _directionsList.Add(Directions.Bottom);
        }
        
        public (Actions, Directions) Decide(IWormInfoProvider worm, IWorldInfoProvider infoProvider)
        {
            var action = Actions.Move;
            var direction = _directionsList[_stepsCounter];
            _stepsCounter = (_stepsCounter + 1) % (_directionsList.Count - 1);

            return (action, direction);
        } 
    }
}