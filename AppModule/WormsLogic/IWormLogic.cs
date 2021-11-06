using ConsoleApp1;

public interface IWormLogic
{
    public (Actions, Directions) Decide(IWormInfoProvider worm, IWorldInfoProvider infoProvider);
}