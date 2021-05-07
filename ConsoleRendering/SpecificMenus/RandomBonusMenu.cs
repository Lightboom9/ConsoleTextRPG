using System;
using System.Collections.Generic;
using ConsoleTextRPG.Characters;
using ConsoleTextRPG.Drop;
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

            WeightedDropTable table = new WeightedDropTable(player);
            for (int i = 0; i < amount; i++)
            {
                IDroppable drop = table.GetRandomDrop();

                _descriptions.Add(drop.GetDropDescription(player));
                _options.Add(() => drop.ApplyToPlayer(player));
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