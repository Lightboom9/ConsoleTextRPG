using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.ConsoleRendering
{
    public class BattleMenu : Menu
    {
        private RandomEnemy _enemy;
        private Player _player;

        public bool PlayerSelectedSkill { get; set; } = false;

        private string _enemyDamageInfo = null;
        private string _playerDamageInfo = null;

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
                void MakePlayerUseSelectedSkill()
                {
                    OnReturnControl -= MakePlayerUseSelectedSkill;

                    if (PlayerSelectedSkill)
                    {
                        player.UseSelectedSkill(enemy);

                        PlayerSelectedSkill = false;
                    }
                }

                OnReturnControl += MakePlayerUseSelectedSkill;

                SkillSelectionMenu menu = new SkillSelectionMenu(this, player);
                Rendering.SetActiveMenu(menu);
            };
        }

        public override string Render()
        {
            /*
            string enemyDamageInfo = _enemy.GetLastReceivedDamageInfo();
            string playerDamageInfo = _player.GetLastReceivedDamageInfo();

            if (enemyDamageInfo != null) _enemyDamageInfo = enemyDamageInfo;
            if (playerDamageInfo != null) _playerDamageInfo = playerDamageInfo;
            */

            string str = $"You have {_player.Health} health and {_player.Mana} mana.\nEnemy has {_enemy.Health} health and {_enemy.Mana} mana.";

            if (_enemyDamageInfo != null || _playerDamageInfo != null)
            {
                str += "\n\nIn last turn:";
            }
            if (_playerDamageInfo != null)
            {
                str += $"\nEYou received: {_playerDamageInfo} damage.";
            }
            if (_enemyDamageInfo != null)
            {
                str += $"\nEnemy received: {_enemyDamageInfo} damage.";
            }

            str += "\n\n[Tab] View info.\n[Space] Select skill to use.";

            return str;
        }
    }
}