using System;
using System.Collections.Generic;

namespace ConsoleApp1.WormsLogic
{
    public class ClockwiseRotationLogic: IWormLogic
    {
        private List<Directions> _directionsList = new List<Directions>();
        private int _stepsCounter = 0;
        
        public ClockwiseRotationLogic()
        {
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
        
        public void Decide(Worm worm, GameContract.FieldObjects[,] gameField)
        {
            var action = Actions.Move;
            var direction = _directionsList[_stepsCounter];
            _stepsCounter = (_stepsCounter + 1) % (_directionsList.Count - 1);

            Console.WriteLine(_stepsCounter);
            Console.WriteLine(action + " " + direction);
            worm.ActionsIntent = action;
            worm.DirectionIntent = direction;
        } 
    }
}