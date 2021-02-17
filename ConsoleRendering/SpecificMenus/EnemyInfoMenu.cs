using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.ConsoleRendering
{
    public class EnemyInfoMenu : Menu
    {
        private RandomEnemy _enemy;

        public EnemyInfoMenu(RandomEnemy enemy)
        {
            _enemy = enemy;

            Actions[ConsoleKey.C] = Actions[ConsoleKey.Spacebar] = ReturnControl;
        }

        public override string Render()
        {
            string str = $"Enemy has {_enemy.Health} health and {_enemy.Mana} mana. They possess {_enemy.Skills.Count} skills.";

            if (_enemy.Weaknesses.Length > 0)
            {
                str += "\nWeaknesses: ";
                for (int i = 0; i < _enemy.Weaknesses.Length; i++)
                {
                    if (i > 0) str += ", ";
                    str += _enemy.Weaknesses[i];
                }
                str += ".";
            }

            if (_enemy.Strengths.Length > 0)
            {
                str += "\nStrengths: ";
                for (int i = 0; i < _enemy.Strengths.Length; i++)
                {
                    if (i > 0) str += ", ";
                    str += _enemy.Strengths[i];
                }
                str += ".";
            }

            str += "\n\nPress [Space] or [C] to return.";

            return str;
        }
    }
}