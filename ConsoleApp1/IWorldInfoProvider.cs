using System.Collections.Generic;

namespace ConsoleApp1
{
    public interface IWorldInfoProvider
    {
        List<IWormInfoProvider> ProvideWormsInfo();
        List<IFoodInfoProvider> ProvideFood();
        GameField ProvideGameField();
        int ProvideGameIteration();
    }
}