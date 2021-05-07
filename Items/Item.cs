using System;
using ConsoleTextRPG.Characters;
using ConsoleTextRPG.Drop;

namespace ConsoleTextRPG.Items
{
    public abstract class Item : IComparable<Item>, ICloneable, IDroppable
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public virtual int DropWeight => 30;

        public void ApplyToPlayer(Player player)
        {
            player.Items.Add(this);
        }

        public virtual string GetDropDescription(Player player)
        {
            return "DEFAULT_DESCRIPTION";
        }

        public virtual int CompareTo(Item item)
        {
            if (DropWeight > item.DropWeight) return 1;
            if (DropWeight < item.DropWeight) return -1;
            return 0;
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public static Item Generate(int level)
        {
            Random rng = new Random();

            switch (rng.Next(0, 3))
            {
                case 0: return new Sword(level);
                case 1: return new Crystal(level);
                case 2: return new HealthPotion(level);
            }

            return null;
        }
    }
}