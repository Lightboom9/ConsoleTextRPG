using System;
using System.Linq;
using System.Text;
using System.Threading;
using ConsoleTextRPG.Characters;
using ConsoleTextRPG.Skills;
using ConsoleTextRPG.ConsoleRendering;
using ConsoleTextRPG.GameEvents;
using Controllers;

namespace ConsoleTextRPG
{
    class Program
    {
        private static void Main(string[] args)
        {
            Rendering.BeginRenderLoop();

            Player player = new Player(10, 10, 10, 10, 10, 10);
            player.Skills.Add(AbilityInfo.Generate(10));
            player.Skills.Add(AbilityInfo.Generate(10));
            player.Skills.Add(AbilityInfo.Generate(10));

            ExplorationMenu exploration = new ExplorationMenu(player);
            ExplorationMenu exploration2 = new ExplorationMenu(player);
            bool a = exploration == exploration2;

            Rendering.SetActiveMenu(exploration);

            InstantiateDiscord(exploration, player);
        }

        private static void InstantiateDiscord(ExplorationMenu exploration, Player player)
        {
            DiscordController.CreateDiscord();

            while (true)
            {
                int lastWinStreak = exploration.WinStreak;
                int lastPlayerHealth = player.Health;

                if (lastWinStreak > 0)
                {
                    DiscordController.SetStatus("Win streak is " + lastWinStreak + " wins long", "Player has " + lastPlayerHealth + " health left");
                }
                else
                {
                    DiscordController.SetStatus("Player has " + lastPlayerHealth + " health left");
                }

                DiscordController.RunCallbacks();

                Thread.Sleep(100);
            }
        }
    }
}
