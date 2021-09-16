namespace ConsoleApp1.Generators
{
    public static class IdGenerator
    {
        private static long _wormId = 0;

        public static long GetWormId()
        {
            return _wormId++;
        }
    }
}