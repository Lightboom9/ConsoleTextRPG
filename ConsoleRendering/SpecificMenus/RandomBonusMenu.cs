using System;
using System.Collections.Generic;
using ConsoleTextRPG.Characters;
using ConsoleTextRPG.Skills;

namespace ConsoleTextRPG.ConsoleRendering
{
    public class RandomBonusMenu : Menu
    {
        private List<string> _descriptions = new List<string>();
        private List<Action> _options = new List<Action>();

        private int _selectedIndex = 0;

        public RandomBonusMenu(Player player)
        {
            Random rng = new Random();

            int amount = rng.Next(2, 6);
            for (int i = 0; i < amount; i++)
            {
                // Restore health/mana, raise a characteristic, pick a new skill.
                int bonusType = rng.Next(0, 3);

                switch (bonusType)
                {
                    case 0:
                    {
                        // Restores health or mana.
                        int restoreType = rng.Next(0, 2);

                        if (restoreType == 0)
                        {
                            int half = player.MaxHealth / 2;
                            int low = (int) Math.Round(half * 0.75f);
                            int high = (int) Math.Round(half * 1.25f) + 1;
                            int restoreAmount = rng.Next(low, high);

                            _descriptions.Add("Restore " + restoreAmount + " health.");
                            _options.Add(() => { player.Restore(restoreAmount, 0); });
                        }
                        else
                        {
                            int half = player.MaxMana / 2;
                            int low = (int) Math.Round(half * 0.75f);
                            int high = (int) Math.Round(half * 1.25f) + 1;
                            int restoreAmount = rng.Next(low, high);

                            _descriptions.Add("Restore " + restoreAmount + " mana.");
                            _options.Add(() => { player.Restore(0, restoreAmount); });
                        }

                        break;
                    }
                    case 1:
                    {
                        // Strength, agility, intelligence, endurance, wisdom, wits.
                        int raiseType = rng.Next(0, 6);

                        switch (raiseType)
                        {
                            case 0:
                            {
                                float partition = player.BaseStrength * 0.15f;
                                int low = (int) Math.Floor(partition - 1);
                                if (low < 1) low = 1;
                                int high = (int) Math.Ceiling(partition + 1) + 1;
                                if (high <= low + 1) high = low + 2;
                                int raiseAmount = rng.Next(low, high);

                                _descriptions.Add("Raise strength by " + raiseAmount + ".");
                                _options.Add(() => { player.RaiseStats(raiseAmount, 0, 0, 0, 0, 0); });

                                break;
                            }
                            case 1:
                            {
                                float partition = player.BaseAgility * 0.15f;
                                int low = (int) Math.Floor(partition - 1);
                                if (low < 1) low = 1;
                                int high = (int) Math.Ceiling(partition + 1) + 1;
                                if (high <= low + 1) high = low + 2;
                                int raiseAmount = rng.Next(low, high);

                                _descriptions.Add("Raise agility by " + raiseAmount + ".");
                                _options.Add(() => { player.RaiseStats(0, raiseAmount, 0, 0, 0, 0); });

                                break;
                            }
                            case 2:
                            {
                                float partition = player.BaseIntelligence * 0.15f;
                                int low = (int) Math.Floor(partition - 1);
                                if (low < 1) low = 1;
                                int high = (int) Math.Ceiling(partition + 1) + 1;
                                if (high <= low + 1) high = low + 2;
                                int raiseAmount = rng.Next(low, high);

                                _descriptions.Add("Raise intelligence by " + raiseAmount + ".");
                                _options.Add(() => { player.RaiseStats(0, 0, raiseAmount, 0, 0, 0); });

                                break;
                            }
                            case 3:
                            {
                                float partition = player.BaseEndurance * 0.15f;
                                int low = (int) Math.Floor(partition - 1);
                                if (low < 1) low = 1;
                                int high = (int) Math.Ceiling(partition + 1) + 1;
                                if (high <= low + 1) high = low + 2;
                                int raiseAmount = rng.Next(low, high);

                                _descriptions.Add("Raise endurance by " + raiseAmount + ".");
                                _options.Add(() =>
                                {
                                    player.RaiseStats(0, 0, 0, raiseAmount, 0, 0);
                                    player.Restore(raiseAmount * 10, 0);
                                });

                                break;
                            }
                            case 4:
                            {
                                float partition = player.BaseWisdom * 0.15f;
                                int low = (int) Math.Floor(partition - 1);
                                if (low < 1) low = 1;
                                int high = (int) Math.Ceiling(partition + 1) + 1;
                                if (high <= low + 1) high = low + 2;
                                int raiseAmount = rng.Next(low, high);

                                _descriptions.Add("Raise wisdom by " + raiseAmount + ".");
                                _options.Add(() =>
                                {
                                    player.RaiseStats(0, 0, 0, 0, raiseAmount, 0);
                                    player.Restore(0, raiseAmount * 10);
                                });

                                break;
                            }
                            case 5:
                            {
                                float partition = player.BaseWits * 0.15f;
                                int low = (int) Math.Floor(partition - 1);
                                if (low < 1) low = 1;
                                int high = (int) Math.Ceiling(partition + 1) + 1;
                                if (high <= low + 1) high = low + 2;
                                int raiseAmount = rng.Next(low, high);

                                _descriptions.Add("Raise wits by " + raiseAmount + ".");
                                _options.Add(() => { player.RaiseStats(0, 0, 0, 0, 0, raiseAmount); });

                                break;
                            }
                        }

                        break;
                    }
                    case 2:
                    {
                        AbilityInfo skill = AbilityInfo.Generate(player.GetAverageLevel());

                        _descriptions.Add("New skill: " + skill.Name.ToLower() + ". " + player.GetDescription(skill));
                        _options.Add(() => { player.Skills.Add(skill); });

                        break;
                    }
                }
            }

            Actions[ConsoleKey.W] = () =>
            {
                _selectedIndex--;
                if (_selectedIndex < 0) _selectedIndex = _options.Count - 1;
            };
            Actions[ConsoleKey.S] = () =>
            {
                _selectedIndex++;
                if (_selectedIndex >= _options.Count) _selectedIndex = 0;
            };
            Actions[ConsoleKey.Spacebar] = () =>
            {
                _options[_selectedIndex]?.Invoke();

                ReturnControl();
            };
        }

        public override string Render()
        {
            string str = "Select a bonus to acquire.\n";

            for (int i = 0; i < _options.Count; i++)
            {
                str += "\n";
                if (i == _selectedIndex) str += "[Select] ";
                str += _descriptions[i];
            }

            str += "\n\nPress [W]/[S] to move through list, [Space] to select a bonus.";

            return str;
        }
    }
}