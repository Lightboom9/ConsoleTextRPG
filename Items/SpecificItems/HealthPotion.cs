using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.Items
{
    public class HealthPotion : Item, IConsumable
    {
        private int _healthRestored;
        public override int DropWeight => 40;

        public HealthPotion(int level)
        {
            Random rng = new Random();

            int low = (int) Math.Round(level * 2.5f);
            int high = (int)Math.Round(level * 6f);
            _healthRestored = rng.Next(low, high + 1);

            Name = "Health potion";
            Description = "Restores " + _healthRestored + " health.";
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