using System;
using System.Collections.Generic;
using ConsoleTextRPG.Characters;
using ConsoleTextRPG.Items;

namespace ConsoleTextRPG.Drop.SpecificDrops
{
    public class ClonePotion : IDroppable
    {
        public int DropWeight => 1500;

        public string GetDropDescription(Player player)
        {
            return "Clones best healing potion you have, also increasing clone's healing power by 1.5 times.";
        }

        public void ApplyToPlayer(Player player)
        {
            if (player.Items.Count == 0) return;

            List<HealthPotion> potions = new List<HealthPotion>();
            foreach (var item in player.Items)
            {
                if (item is HealthPotion potion) potions.Add(potion);
            }

            if (potions.Count == 0) return;

            HealthPotion bestPotion = potions[0];
            for (int i = 1; i < potions.Count; i++)
            {
                if (bestPotion.CompareTo(potions[i]) == -1)
                {
                    bestPotion = potions[i];
                }
            }

            HealthPotion clonePotion = bestPotion.Clone() as HealthPotion;
            clonePotion.HealthRestored = (int)Math.Round(clonePotion.HealthRestored * 1.5f);

            player.Items.Add(clonePotion);
        }
    }
}