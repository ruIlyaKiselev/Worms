namespace ConsoleApp1.WormsLogic
{
    public interface IWormLogic
    {
        public (Actions, Directions) Decide(IWormInfoProvider worm, IWorldInfoProvider infoProvider);
    }
}