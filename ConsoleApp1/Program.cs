using System;
using ConsoleApp1.Generators;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomName randomName = new RandomName(new Random(DateTime.Now.Second));
            string name = randomName.Generate(Sex.Male);
            Console.WriteLine(name);
            GameController gameController = new GameController();
            gameController.GameProcess();
        }
    }
}
