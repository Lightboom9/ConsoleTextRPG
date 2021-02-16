using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.ConsoleRendering
{
    public class SkillSelectionMenu : Menu
    {
        private static int _cursorMemory = 0;

        private Player _player;

        private int _selectedIndex = _cursorMemory;

        public SkillSelectionMenu(BattleMenu battleMenuParent, Player player) : base(battleMenuParent)
        {
            _player = player;

            Actions[ConsoleKey.Spacebar] = () =>
            {
                player.NextSelectedSkillToUse = _selectedIndex;
                battleMenuParent.PlayerSelectedSkill = true;

                _cursorMemory = _selectedIndex;

                ReturnControl();
            };
            Actions[ConsoleKey.W] = () =>
            {
                _selectedIndex--;
                if (_selectedIndex < 0) _selectedIndex = player.Skills.Count - 1;
            };
            Actions[ConsoleKey.S] = () =>
            {
                _selectedIndex++;
                if (_selectedIndex >= player.Skills.Count) _selectedIndex = 0;
            };
            Actions[ConsoleKey.C] = ReturnControl;
        }

        public override string Render()
        {
            string str = "";

            str += "You have " + _player.Skills.Count + " skills.\n\n";
            for (var i = 0; i < _player.Skills.Count; i++)
            {
                var skill = _player.Skills[i];
                if (_selectedIndex == i) str += "[Select] ";
                str += skill.Name + ": " + _player.GetDescription(skill) + "\n";
            }

            str += "\n[W]/[S] to move through list, [Space] to select skill, [C] to cancel.";

            return str;
        }
    }
}