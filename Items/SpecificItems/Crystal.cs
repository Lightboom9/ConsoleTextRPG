using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.Items
{
    public class Crystal : Item, IEquippable, IConsumable
    {
        private int _manaRestored;
        public override int DropWeight => 25;

        public bool Equipped { get; set; }
        public int StrengthBonus => 0;
        public int IntelligenceBonus { get; private set; }
        public int AgilityBonus => 0;
        public int EnduranceBonus => 0;
        public int WisdomBonus => 0;
        public int WitsBonus => 0;

        public Crystal(int level)
        {
            Random rng = new Random();

            int low = (int)Math.Round(level * 3f);
            int high = (int)Math.Round(level * 6.5f);
            _manaRestored = rng.Next(low, high + 1);

            low = (int)Math.Max(2, level / 5f);
            high = (int)Math.Round(level / 2.5f);
            IntelligenceBonus = rng.Next(low, high + 2);

            Name = "Crystal";
            Description = "Increases intelligence by " + IntelligenceBonus + ". If consumed, restores " + _manaRestored + " mana.";
        }

        public void Consume(Player player)
        {
            player.Restore(0, _manaRestored);
        }

        public override string GetDropDescription(Player player)
        {
            return "Item: crystal. Increases intelligence by " + IntelligenceBonus + ". If consumed, restores " + _manaRestored + " mana.";
        }
    }
}