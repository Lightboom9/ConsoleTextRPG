using System;
using ConsoleTextRPG.Characters;
using ConsoleTextRPG.Drop;

namespace ConsoleTextRPG.Items
{
    public abstract class Item : IComparable<Item>, ICloneable, IDroppable
    {
        public string Name { get; protected set; }
        public virtual int DropWeight => 30;

        public void ApplyToPlayer(Player player)
        {
            player.Items.Add(this);
        }

        public string GetDropDescription(Player player)
        {
            return "Item";
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

        protected abstract Item GenerateItem();

        public static Item Generate(int level)
        {
            return null;
        }
    }
}