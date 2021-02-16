using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.ConsoleRendering
{
    public class BattleMenu : Menu
    {
        private Character _enemy;
        private Player _player;

        public bool PlayerSelectedSkill { get; set; } = false;

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
                OnReturnControl = () =>
                {
                    OnReturnControl = null;

                    if (PlayerSelectedSkill) player.UseSelectedSkill(enemy);
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