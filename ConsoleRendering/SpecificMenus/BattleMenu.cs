using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.ConsoleRendering
{
    public class BattleMenu : Menu
    {
        private RandomEnemy _enemy;
        private Player _player;

        public bool PlayerSelectedSkill { get; set; } = false;

        public BattleMenu(Player player, RandomEnemy enemy)
        {
            _enemy = enemy;
            _player = player;

            Actions[ConsoleKey.Tab] = () =>
            {
                EnemyInfoMenu menu = new EnemyInfoMenu(this, enemy);
                Rendering.SetActiveMenu(menu);
            };
            Actions[ConsoleKey.Spacebar] = () =>
            {
                OnReturnControl = () =>
                {
                    OnReturnControl = null;

                    if (PlayerSelectedSkill)
                    {
                        player.UseSelectedSkill(enemy);

                        PlayerSelectedSkill = false;
                    }
                };

                SkillSelectionMenu menu = new SkillSelectionMenu(this, player);
                Rendering.SetActiveMenu(menu);
            };
        }

        public override string Render()
        {
            string str = "Enemy has " + _enemy.Health + " health.\n\n[Tab] View info.\n[Space] Select skill to use.";

            return str;
        }
    }
}