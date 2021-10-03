﻿using System.Collections.Generic;

namespace ConsoleApp1
{
    public interface IWorldInfoProvider
    {
        List<IWormInfoProvider> ProvideWorms();
        List<IFoodInfoProvider> ProvideFood();
        GameField ProvideGameField();
        int ProvideGameIteration();
    }
}