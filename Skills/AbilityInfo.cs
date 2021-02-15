using System;

namespace SharpLabProject.Skills
{
    public class AbilityInfo
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public AbilityAttack[] Attacks { get; private set; }
        public int Healing { get; private set; }

        public float Accuracy { get; private set; }
        public float CritChance { get; private set; }

        public int ManaCost { get; private set; }
        public int HealthCost { get; private set; }

        private AbilityInfo() { }

        /// <summary>
        /// Generates a stat, returning a value around 100 by default.
        /// </summary>
        /// <param name="luck">Luck affects the result, making it better the higher it is. Ranges from -1 to 1, with 0 being default boring result.</param>
        /// <param name="min">Lower border, inclusive.</param>
        /// <param name="max">Upper border, exclusive.</param>
        /// <returns></returns>
        private static float GenerateStat(float luck = 0, int min = 25, int max = 175)
        {
            Random rng = new Random();

            luck = Math.Clamp(luck, -1, 1);
            luck *= rng.Next(4, 6);
            float low = 75;
            float high = 125;

            if (luck > 0)
            {
                luck = (float)Math.Pow(luck, 2);

                low += luck / 3f;
                high += luck;
            }
            else
            {
                luck = -(float)Math.Pow(luck, 2);

                low -= luck;
                high -= luck / 3f;
            }

            return rng.Next((int)low, (int)high);
        }

        private static void GenerateSkillDescriptions(AbilityInfo info)
        {
            Random rng = new Random();

            string name = "";
            string description = "";

            // Name generation.
            if (info.Attacks.Length > 0)
            {
                string name1 = null;
                string name2 = null;

                foreach (var atk in info.Attacks)
                {
                    if (name1 == null)
                    {
                        name1 = atk.type.ToString();
                    }
                    else
                    {
                        name2 = atk.type.ToString();
                        if (name2 == name1) name2 = null;
                    }
                }

                name = AbilityNameGenerator.GenerateAttackName(name1, name2);
            }
            else
            {
                name = AbilityNameGenerator.GenerateSupportName();
            }

            //Description generation.
            if (info.Attacks.Length > 0)
            {
                description = "Deals";

                int attackNumber = 1;
                foreach (var atk in info.Attacks)
                {
                    if (attackNumber > 1) description += ",";
                    description += $" [Atk{attackNumber}] {atk.type.ToString().ToLower()}";
                    attackNumber++;
                }
                description += " damage.";
            }
            if (info.Healing > 0)
            {
                if (info.Attacks.Length > 0) description += " ";
                description += $"Heals caster for [Heal].";
            }

            if (info.CritChance >= 10)
            {
                if (info.CritChance >= 20)
                {
                    description += " Very high crit chance.";
                }
                else
                {
                    description += " High crit chance.";
                }
            }
            if (info.Accuracy >= 110)
            {
                if (info.Accuracy >= 140)
                {
                    description += " Very high accuracy.";
                }
                else
                {
                    description += " High accuracy.";
                }
            }

            description += " Costs";
            if (info.HealthCost > 0 && info.ManaCost > 0)
            {
                description += $" {info.HealthCost} health and {info.ManaCost} mana.";
            }
            else
            {
                if (info.HealthCost > 0)
                {
                    description += $" {info.HealthCost} health.";
                }
                else
                {
                    description += $" {info.ManaCost} mana.";
                }
            }

            info.Name = name;
            info.Description = description;
        }
        public static AbilityInfo Generate(int basePower)
        {
            Random rng = new Random();

            bool magic = rng.Next(0, 2) == 0;
            float luck = 0;

            float power = 0;
            float crit = 0;
            float accuracy = 0;
            DamageType type = (DamageType)rng.Next(0, 6);

            // 0-6 default, 7-8 success, 9-10 failure, 11-13 unstable
            int randomCase = rng.Next(0, 14);

            if (randomCase <= 6)
            {
                luck += rng.Next(-5, 6) / 100f;
            }
            else
            {
                if (randomCase <= 8)
                {

                }
                else
                {
                    if (randomCase <= 10)
                    {

                    }
                    else
                    {

                    }
                }
            }

            return null;
        }
    }
}