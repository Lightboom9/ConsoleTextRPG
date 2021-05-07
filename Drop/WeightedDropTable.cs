using System;
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
            _drops = new List<IDroppable>(100);
            _rng = new Random();

            for (int i = 0; i < 100; i++)
            {
                IDroppable drop = null;

                switch (_rng.Next(0, 4))
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
                        
                        // Test
                        continue;

                        break;
                    }
                    default: throw new Exception("Wtf");
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
                    return drop;
                }
            }

            return null;
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