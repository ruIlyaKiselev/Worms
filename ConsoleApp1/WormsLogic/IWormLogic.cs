namespace ConsoleApp1.WormsLogic
{
    public interface IWormLogic
    {
        public void Decide(Worm worm, GameContract.FieldObjects[,] gameField);
    }
}