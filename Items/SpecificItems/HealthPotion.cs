using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.Items
{
    public class HealthPotion : Item, IConsumable, ICloneable, IComparable<HealthPotion>
    {
        private int _healthRestored;
        public override int DropWeight => 4000;

        public int HealthRestored
        {
            get => _healthRestored;
            set
            {
                _healthRestored = value;
                Description = "Restores " + _healthRestored + " health";
            }
        }

        public HealthPotion(int level)
        {
            Random rng = new Random();

            int low = (int) Math.Round(level * 2.5f);
            int high = (int)Math.Round(level * 6f);
            _healthRestored = rng.Next(low, high + 1);

            Name = "Health potion";
            Description = "Restores " + _healthRestored + " health";
        }

        public object Clone()
        {
            HealthPotion potion = new HealthPotion(0);
            potion.HealthRestored = _healthRestored;

            return potion;
        }

        public virtual int CompareTo(HealthPotion potion)
        {
            if (HealthRestored > potion.HealthRestored) return 1;
            if (HealthRestored < potion.HealthRestored) return -1;
            return 0;
        }

        public void Consume(Player player)
        {
            player.Restore(_healthRestored, 0);
        }

        public override string GetDropDescription(Player player)
        {
            return "Item: health potion. Restores " + _healthRestored + " health.";
        }
    }
}