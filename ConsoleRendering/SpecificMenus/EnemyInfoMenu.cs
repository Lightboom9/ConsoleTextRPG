using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.ConsoleRendering
{
    public class EnemyInfoMenu : Menu
    {
        private Character _enemy;

        public EnemyInfoMenu(BattleMenu battleMenuParent, Character enemy) : base(battleMenuParent)
        {
            _enemy = enemy;

            Actions[ConsoleKey.Spacebar] = ReturnControl;
        }

        public override string Render()
        {
            string str = "Enemy has " + _enemy.Health + " health and " + _enemy.Mana + " mana. He possesses " + _enemy.Skills.Count + " skills.\n\n";


            return str;
        }
    }
}