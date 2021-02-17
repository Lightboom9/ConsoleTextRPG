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
            Rendering.BeginRenderLoop();

            Player player = new Player(10, 10, 10, 10, 10, 10);
            player.Skills.Add(AbilityInfo.Generate(10));
            player.Skills.Add(AbilityInfo.Generate(10));
            player.Skills.Add(AbilityInfo.Generate(10));

            ExplorationMenu exploration = new ExplorationMenu(player);

            Rendering.SetActiveMenu(exploration);
        }
    }
}
