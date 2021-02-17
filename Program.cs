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

            RandomEnemy enemy = RandomEnemy.Generate(10);

            player.EnterFight();
            enemy.EnterFight();

            ExplorationMenu exploration = new ExplorationMenu();

            BattleMenu battle = new BattleMenu(exploration, player, enemy);
            Rendering.SetActiveMenu(battle);


        }
    }
}
