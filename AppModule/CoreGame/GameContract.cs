namespace ConsoleApp1.CoreGame
{
    /// <summary>
    ///     Класс-контракт со свойствами (правилами) игры.
    /// </summary>
    public static class GameContract
    {
        /// <summary>NumberOfSteps - число ходов в мире.</summary>
        public static readonly int NumberOfSteps = 100;
        
        /// <summary>Mu - параметр для нормального распределения.</summary>
        public static readonly double Mu = 0;
        /// <summary>Sigma - параметр для нормального распределения.</summary>
        public static readonly double Sigma = 5;

        /// <summary>StartWormHealth - стартовое здоровье червя.</summary>
        public static readonly int StartWormHealth = 10;
        /// <summary>StartFoodHealth - стартовое здоровье еды.</summary>
        public static readonly int StartFoodHealth = 10;
        /// <summary>FoodSaturation - насыщение едой (сколько здоровья еда прибавляет).</summary>
        public static readonly int FoodSaturation = 10;
        /// <summary>WormBuddingDamage - урон при размножении (сколько здоровья отнимается у червя при размножении).</summary>
        public static readonly int WormBuddingDamage = 10;
        /// <summary>HealthDecreasePerIteration - отнимание здоровья у живых объектов за один ход мира.</summary>
        public static readonly int HealthDecreasePerIteration = 1;
    }
}