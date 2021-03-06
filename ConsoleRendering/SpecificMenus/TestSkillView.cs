﻿using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.ConsoleRendering
{
    public class TestSkillView : Menu
    {
        private int _selectedSkill = 0;

        private Player _player;

        public TestSkillView(Player player)
        {
            _player = player;

            Actions[ConsoleKey.W] = () =>
            {
                _selectedSkill--;
                if (_selectedSkill < 0) _selectedSkill = player.Skills.Count - 1;
            };
            Actions[ConsoleKey.S] = () =>
            {
                _selectedSkill++;
                if (_selectedSkill >= player.Skills.Count) _selectedSkill = 0;
            };
        }

        public override string Render()
        {
            string str = "";

            if (_player.Skills.Count > 0)
            {
                for (int i = 0; i < _player.Skills.Count; i++)
                {
                    if (_selectedSkill == i) str += "[X] ";
                    str += _player.Skills[i].Name + ": " + _player.GetDescription(_player.Skills[i]) + "\n";
                }
            }
            else
            {
                str = "Player has no skills.";
            }

            return str;
        }
    }
}