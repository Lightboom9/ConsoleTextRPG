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

        public BattleMenu(ExplorationMenu explorationMenu, Player player, RandomEnemy enemy) : base(explorationMenu)
        {
            Rendering.LockInput();

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

            player.OnTurnEnd += EnemyTurn;
            enemy.OnTurnEnd += PlayerTurn;
            PlayerTurn();
        }

        private void PlayerTurn()
        {
            if (!_enemy.IsAlive)
            {
                ReturnControl();

                return;
            }

            Rendering.UnlockInput();

            _player.StartTurn(new Character[] { _enemy });
        }
        private void EnemyTurn()
        {
            if (!_player.IsAlive)
            {
                ReturnControl();

                return;
            }

            Rendering.LockInput();

            _enemy.StartTurn(new Character[] { _player });
        }

        public override string Render()
        {
            string str = $"You have {_player.Health} health and {_player.Mana} mana.\nEnemy has {_enemy.Health} health and {_enemy.Mana} mana.";

            /*
            string enemyDamageInfo = _enemy.GetLastReceivedDamageInfo();
            string playerDamageInfo = _player.GetLastReceivedDamageInfo();

            if (enemyDamageInfo != null) _enemyDamageInfo = enemyDamageInfo;
            if (playerDamageInfo != null) _playerDamageInfo = playerDamageInfo;

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
            */

            str += "\n\n[Tab] View info.\n[Space] Select skill to use.";

            return str;
        }
    }
}