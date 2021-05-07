using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.Items
{
    public class Sword : Item, IEquippable
    {
        public bool Equipped { get; set; }

        public int StrengthBonus { get; private set; }
        public int IntelligenceBonus => 0;
        public int AgilityBonus => 0;
        public int EnduranceBonus => 0;
        public int WisdomBonus => 0;
        public int WitsBonus => 0;

        public Sword(int level)
        {
            Random rng = new Random();

            int low = (int) Math.Max(2, level / 5f);
            int high = (int) Math.Round(level / 2.5f);
            StrengthBonus = rng.Next(low, high + 2);

            Name = "Sword";
            Description = "Increases the strength by " + StrengthBonus + ".";
        }

        public override string GetDropDescription(Player player)
        {
            return "Item: sword. Increases the strength by " + StrengthBonus + ".";
        }
    }
}