using System.Collections.Generic;

namespace ConsoleApp1.WormsLogic
{
    public class ClockwiseRotationLogic: IWormLogic
    {
        private readonly List<Directions> _directionsList = new();
        private int _stepsCounter;
        
        public ClockwiseRotationLogic()
        {
            _stepsCounter = 0;
            
            if (GameContract.StartX <= -1 && GameContract.FinishX >= 1 && GameContract.StartY <= -1 &&
                GameContract.FinishY >= 1)
            {
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
            else
            {
                _directionsList.Add(Directions.None);
            }
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