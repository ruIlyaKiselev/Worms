using ConsoleApp1;
using ConsoleApp1.WormsLogic;

public interface IWormLogic
{
    public (Actions, Directions) Decide(IWormInfoProvider worm, IWorldInfoProvider infoProvider);
}
