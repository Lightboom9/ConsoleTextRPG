using System;
using System.Threading;
using SharpLabProject.Characters;
using SharpLabProject.Skills;

namespace SharpLabProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(10, 10, 10, 10, 10, 10);

            while (true)
            {
                if (!Console.KeyAvailable) Thread.Sleep(100);

                if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                {
                    Console.Clear();

                    AbilityInfo info = AbilityInfo.Generate(10);

                    Console.WriteLine(info.Name);
                    Console.WriteLine(player.GetDescription(info));
                }
            }
        }
    }
}
