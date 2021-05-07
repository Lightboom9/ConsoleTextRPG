using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.Drop.SpecificDrops
{
    public class StatBoost : IDroppable
    {
        public int DropWeight => 70;

        public int StrengthBoost { get; private set; }
        public int IntelligenceBoost { get; private set; }
        public int AgilityBoost { get; private set; }
        public int EnduranceBoost { get; private set; }
        public int WisdomBoost { get; private set; }
        public int WitsBoost { get; private set; }

        public string GetDropDescription(Player player)
        {
            string str = "";

            if (StrengthBoost > 0) str = "Increase your strength by " + StrengthBoost + ".";
            if (IntelligenceBoost > 0) str = "Increase your intelligence by " + IntelligenceBoost + ".";
            if (AgilityBoost > 0) str = "Increase your agility by " + AgilityBoost + ".";
            if (EnduranceBoost > 0) str = "Increase your endurance by " + EnduranceBoost + ".";
            if (WisdomBoost > 0) str = "Increase your wisdom by " + WisdomBoost + ".";
            if (WitsBoost > 0) str = "Increase your wits by " + WitsBoost + ".";

            return str;
        }

        public void ApplyToPlayer(Player player)
        {
            player.RaiseStats(StrengthBoost, AgilityBoost, IntelligenceBoost, EnduranceBoost, WisdomBoost, WitsBoost);
        }

        private StatBoost() { }

        public static StatBoost Generate(int level)
        {
            Random rng = new Random();
            StatBoost boost = new StatBoost();

            switch (rng.Next(0, 6))
            {
                case 0:
                {
                    boost.StrengthBoost = rng.Next(1, (int)Math.Round(level / 10f));

                    break;
                }
                case 1:
                {
                    boost.IntelligenceBoost = rng.Next(1, (int)Math.Round(level / 10f));

                    break;
                }
                case 2:
                {
                    boost.AgilityBoost = rng.Next(1, (int)Math.Round(level / 10f));

                    break;
                }
                case 3:
                {
                    boost.EnduranceBoost = rng.Next(1, (int)Math.Round(level / 10f));

                    break;
                }
                case 4:
                {
                    boost.WisdomBoost = rng.Next(1, (int)Math.Round(level / 10f));

                    break;
                }
                case 5:
                {
                    boost.WitsBoost = rng.Next(1, (int)Math.Round(level / 10f));

                    break;
                }
            }

            return boost;
        }
    }
}