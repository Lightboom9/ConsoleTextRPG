using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.ConsoleRendering
{
    public class ExplorationMenu : Menu
    {
        private Player _player;

        private bool? _fightWasWon = null;
        private int _winStreak;

        public ExplorationMenu(Player player)
        {
            _player = player;

            OnReturnControl += () =>
            {
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                string msg = GetLastReturnControlMessage();
                
                if (msg == null) return;

                if (msg == "Victory")
                {
                    _fightWasWon = true;

                    _winStreak++;
                }
                else
                {
                    if (msg == "Lose")
                    {
                        _fightWasWon = false;
                    }
                }
            };

            Actions[ConsoleKey.Spacebar] = () =>
            {
                if (_fightWasWon == null)
                {
                    StartRandomFight();
                }
                else
                {
                    if (_fightWasWon == true)
                    {
                        RandomBonusMenu bonusMenu = new RandomBonusMenu(player);
                        HandleControl(bonusMenu);
                    }
                    else
                    {
                        _winStreak = 0;

                        _player.FullRevive();
                    }

                    _fightWasWon = null;
                }
            };
        }

        private void StartRandomFight()
        {
            Random rng = new Random();

            int averageLevel = (int)Math.Round(_player.GetAverageLevel() / 1.66f);
            RandomEnemy enemy = RandomEnemy.Generate(rng.Next(averageLevel - 1, averageLevel + 2));

            _player.EnterFight();
            enemy.EnterFight();

            BattleMenu battle = new BattleMenu(_player, enemy);
            HandleControl(battle);
        }

        public override string Render()
        {
            string str = "";

            if (_fightWasWon == null)
            {
                if (_winStreak > 0) str += $"Your winstreak is { _winStreak } wins long. You have { _player.Health } health and { _player.Mana } mana left.\n\n";
                str += "Press [Space] to enter random fight.";
            }
            else
            {
                if (_fightWasWon == true)
                {
                    str += "You won the fight, congrats! Pick a bonus.\n\nPress [Space] to continue.";
                }
                else
                {
                    str += "You lost the fight, too bad. Health and mana are restored, feel free to try again.\n\nPress [Space] to continue.";
                }
            }
            
            return str;
        }
    }
}