using System;
using System.Linq;
using System.Text;
using System.Threading;
using ConsoleTextRPG.Characters;
using ConsoleTextRPG.Skills;
using ConsoleTextRPG.ConsoleRendering;
using ConsoleTextRPG.GameEvents;

namespace ConsoleTextRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            //Rendering.BeginRenderLoop();

            Player player = new Player(10, 10, 10, 10, 10, 10);
            RandomBattle battle = RandomBattle.Generate(8);
            Character enemy = battle.Enemy;

            Console.WriteLine("Enemy has " + enemy.Health + ". His skills are:");
            foreach (var skill in enemy.Skills)
            {
                Console.WriteLine(skill.Description);
            }
        }
    }
}
