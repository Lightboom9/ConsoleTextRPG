using System;
using ConsoleTextRPG.Characters;

namespace ConsoleTextRPG.Drop.SpecificDrops
{
    public class Restoration : IDroppable
    {
        public int DropWeight => 75;

        public int HealthRestored { get; private set; }
        public int ManaRestored { get; private set; }

        public string GetDropDescription(Player player)
        {
            string str = "Restore ";

            if (HealthRestored > 0)
            {
                str += HealthRestored + " health";
                if (ManaRestored > 0) str += " and " + ManaRestored + " mana";
            }
            else
            {
                str += ManaRestored + " mana";
            }

            return str + ".";
        }

        public void ApplyToPlayer(Player player)
        {
            player.Restore(HealthRestored, ManaRestored);
        }

        private Restoration() { }

        public static Restoration Generate(Player player)
        {
            Random rng = new Random();
            Restoration rest = new Restoration();

            switch (rng.Next(0, 2))
            {
                case 0:
                {
                    int half = player.MaxHealth / 2;
                    int low = (int)Math.Round(half * 0.75f);
                    int high = (int)Math.Round(half * 1.25f) + 1;
                    rest.HealthRestored = rng.Next(low, high);

                    break;
                }
                case 1:
                {
                    int half = player.MaxMana / 2;
                    int low = (int) Math.Round(half * 0.75f);
                    int high = (int) Math.Round(half * 1.25f) + 1;
                    rest.ManaRestored = rng.Next(low, high);

                    break;
                }
            }

            return rest;
        }
    }
}