using System;
using ConsoleTextRPG.Characters;
using ConsoleTextRPG.Items;

namespace ConsoleTextRPG.ConsoleRendering
{
    public class ItemsMenu : Menu
    {
        private static int _cursorMemory = 0;

        private Player _player;

        private int _selectedIndex = _cursorMemory;

        private bool _currentIsEquippable = false;
        private bool _currentIsConsumable = false;

        public ItemsMenu(Player player)
        {
            _player = player;

            Item selectedItem = player.Items[_cursorMemory];
            _currentIsEquippable = selectedItem is IEquippable;
            _currentIsConsumable = selectedItem is IConsumable;

            Actions[ConsoleKey.Spacebar] = () =>
            {
                // Consume
                if (!_currentIsConsumable) return;

                if (_currentIsEquippable)
                {
                    IEquippable equippable = player.Items[_selectedIndex] as IEquippable;
                    equippable.Equipped = false;
                }

                IConsumable consumable = player.Items[_selectedIndex] as IConsumable;
                consumable.Consume(player);
                player.Items.RemoveAt(_selectedIndex);

                FullRerenderRequested = true;
            };
            Actions[ConsoleKey.Z] = () =>
            {
                // Equip
                if (!_currentIsEquippable) return;

                foreach (var item in player.Items)
                {
                    if (item is IEquippable equip)
                    {
                        equip.Equipped = false;
                    }
                }
                IEquippable equippable = player.Items[_selectedIndex] as IEquippable;
                equippable.Equipped = true;

                FullRerenderRequested = true;
            };
            Actions[ConsoleKey.W] = () =>
            {
                _selectedIndex--;
                if (_selectedIndex < 0) _selectedIndex = player.Items.Count - 1;

                Item selectedItem = player.Items[_selectedIndex];
                _currentIsEquippable = selectedItem is IEquippable;
                _currentIsConsumable = selectedItem is IConsumable;
            };
            Actions[ConsoleKey.S] = () =>
            {
                _selectedIndex++;
                if (_selectedIndex >= player.Items.Count) _selectedIndex = 0;

                Item selectedItem = player.Items[_selectedIndex];
                _currentIsEquippable = selectedItem is IEquippable;
                _currentIsConsumable = selectedItem is IConsumable;
            };
            Actions[ConsoleKey.C] = ReturnControl;
        }

        public override string Render()
        {
            string str = "";

            str += "You have " + _player.Items.Count + " items.\n\n";
            for (var i = 0; i < _player.Items.Count; i++)
            {
                var item = _player.Items[i];
                if (_selectedIndex == i)
                {
                    str += "[Select] ";
                    if (_currentIsEquippable)
                    {
                        IEquippable equippable = item as IEquippable;
                        if (equippable.Equipped) str += "{Equipped} ";
                    }
                }
                str += item.Name + ". " + item.Description + ".\n";
            }

            str += "\nPress [W]/[S] to move through list, ";
            if (_currentIsConsumable) str += "[Space] to select consume, ";
            if (_currentIsEquippable) str += "[Z] to select equip, ";
            str += "[C] to return.";

            return str;
        }
    }
}