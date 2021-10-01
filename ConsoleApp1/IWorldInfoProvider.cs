using System.Collections.Generic;

namespace ConsoleApp1
{
    public interface IWorldInfoProvider
    {
        List<Worm> ProvideWorms();
        List<Food> ProvideFood();
        GameField ProvideGameField();
        int ProvideGameIteration();
    }
}