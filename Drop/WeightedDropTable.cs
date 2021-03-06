﻿using System;
using System.Collections.Generic;
using ConsoleTextRPG.Characters;
using ConsoleTextRPG.Drop.SpecificDrops;
using ConsoleTextRPG.Items;
using ConsoleTextRPG.Skills;

namespace ConsoleTextRPG.Drop
{
    public class WeightedDropTable
    {
        private List<IDroppable> _drops;
        private Random _rng;

        private int _totalWeight = 0;

        public WeightedDropTable(Player player)
        {
            _drops = new List<IDroppable>(150);
            _rng = new Random();

            for (int i = 0; i < 150; i++)
            {
                IDroppable drop = null;

                switch (_rng.Next(0, 5))
                {
                    case 0:
                    {
                        // Restoration
                        drop = Restoration.Generate(player);

                        break;
                    }
                    case 1:
                    {
                        // Stat boost
                        drop = StatBoost.Generate(player.GetAverageLevel());

                        break;
                    }
                    case 2:
                    {
                        // Skill
                        drop = AbilityInfo.Generate(player.GetAverageLevel());

                        break;
                    }
                    case 3:
                    {
                        // Item
                        drop = Item.Generate(player.GetAverageLevel());

                        break;
                    }
                    case 4:
                    {
                        // Clone potion
                        drop = new ClonePotion();

                        break;
                    }
                }

                _drops.Add(drop);
                _totalWeight += drop.DropWeight;
            }
        }

        public IDroppable GetRandomDrop()
        {
            int selectedWeight = _rng.Next(0, _totalWeight + 1);
            int currentWeight = 0;

            foreach (var drop in _drops)
            {
                currentWeight += drop.DropWeight;

                if (currentWeight >= selectedWeight)
                {
                    _drops.Remove(drop);
                    _totalWeight -= drop.DropWeight;

                    return drop;
                }
            }

            throw new SystemException("Should not happen. Total weight: " + _totalWeight + ", selected weight: " + selectedWeight + ", current weight: " + currentWeight + ".\n");
        }

        public IDroppable[] GetRandomDrops(int amount)
        {
            IDroppable[] drops = new IDroppable[amount];

            for (int i = 0; i < amount; i++)
            {
                drops[i] = GetRandomDrop();
            }

            return drops;
        }
    }
}