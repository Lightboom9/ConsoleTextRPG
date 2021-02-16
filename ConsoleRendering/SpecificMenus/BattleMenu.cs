using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.ConsoleRendering
{
    public class BattleMenu : Menu
    {
        private Character _enemy;
        private Player _player;

        public BattleMenu(Player player, Character enemy)
        {
            _enemy = enemy;
            _player = player;

            Actions[ConsoleKey.Tab] = () =>
            {
                // Enemy info.
            };
            Actions[ConsoleKey.Spacebar] = () =>
            {
                // Skill selection
            };
        }

        public override string Render()
        {
            string str = "Enemy has " + _enemy.Health + " health.\n\n[Tab] View info.\n[Space] Select skill to use.";

            return str;
        }
    }
}