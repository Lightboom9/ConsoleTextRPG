using System;

namespace ConsoleTextRPG.Skills
{
    public class AbilityInfo
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public AbilityAttack[] Attacks { get; private set; }
        public int Healing { get; private set; }

        public int AverageAccuracy { get; private set; }
        public int AverageCritChance { get; private set; }

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
            float low = min;
            float high = max;

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

            if (info.AverageCritChance >= 10)
            {
                if (info.AverageCritChance >= 20)
                {
                    description += " Very high crit chance.";
                }
                else
                {
                    description += " High crit chance.";
                }
            }
            if (info.AverageAccuracy >= 110)
            {
                if (info.AverageAccuracy >= 140)
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

            int attackCount = rng.Next(0, 41);
            if (attackCount <= 30)
            {
                attackCount = 1;
            }
            else
            {
                if (attackCount <= 38)
                {
                    attackCount = 2;
                }
                else
                {
                    attackCount = 3;
                }
            }

            AbilityAttack[] attacks = new AbilityAttack[attackCount];

            float totalAccuracy = 0;
            float totalCrit = 0;
            float totalHealthCost = 0;
            float totalManaCost = 0;

            for (int i = 0; i < attackCount; i++)
            {
                bool magic = rng.Next(0, 2) == 0;
                float luck = 0;

                float power = 0;
                float crit = 0;
                float accuracy = 0;
                float healthCost = 0;
                float manaCost = 0;
                DamageType type = magic ? (DamageType) rng.Next(3, 6) : (DamageType) rng.Next(0, 2);


                // 0=power-accuracy-crit, 1=power-crit-accuracy, 2=crit-power-accuracy, 3=accuracy-crit-power
                int order = rng.Next(0, 4);

                // 0-6 default, 7-8 success, 9-10 failure, 11-13 unstable
                int randomCase = rng.Next(0, 14);

                if (randomCase <= 6)
                {
                    switch (order)
                    {
                        case 0:
                        {
                            luck += rng.Next(-5, 6) / 100f;
                            power = GenerateStat(luck, 70, 130) / 100f * basePower;

                            if (magic)
                            {
                                luck += rng.Next(-5, 6) / 100f;
                                accuracy = GenerateStat(luck, 90, 100);

                                luck += rng.Next(-5, 6) / 100f;
                                crit = GenerateStat(luck, 90, 110) / 25f;
                            }
                            else
                            {
                                luck += rng.Next(-5, 6) / 100f;
                                accuracy = GenerateStat(luck, 70, 130);

                                luck += rng.Next(-5, 6) / 100f;
                                crit = GenerateStat(luck, 30, 150) / 10f;
                            }

                            break;
                        }
                        case 1:
                        {
                            luck += rng.Next(-5, 6) / 100f;
                            power = GenerateStat(luck, 70, 130) / 100f * basePower;

                            if (magic)
                            {
                                luck += rng.Next(-5, 6) / 100f;
                                crit = GenerateStat(luck, 90, 110) / 25f;

                                luck += rng.Next(-5, 6) / 100f;
                                accuracy = GenerateStat(luck, 90, 100);
                            }
                            else
                            {
                                luck += rng.Next(-5, 6) / 100f;
                                crit = GenerateStat(luck, 30, 150) / 10f;

                                luck += rng.Next(-5, 6) / 100f;
                                accuracy = GenerateStat(luck, 70, 130);
                            }

                            break;
                        }
                        case 2:
                        {
                            if (magic)
                            {
                                luck += rng.Next(-5, 6) / 100f;
                                crit = GenerateStat(luck, 90, 110) / 25f;

                                luck += rng.Next(-5, 6) / 100f;
                                power = GenerateStat(luck, 70, 130) / 100f * basePower;

                                luck += rng.Next(-5, 6) / 100f;
                                accuracy = GenerateStat(luck, 90, 100);
                            }
                            else
                            {
                                luck += rng.Next(-5, 6) / 100f;
                                crit = GenerateStat(luck, 30, 150) / 10f;

                                luck += rng.Next(-5, 6) / 100f;
                                power = GenerateStat(luck, 70, 130) / 100f * basePower;

                                luck += rng.Next(-5, 6) / 100f;
                                accuracy = GenerateStat(luck, 70, 130);
                            }

                            break;
                        }
                        case 3:
                        {
                            if (magic)
                            {
                                luck += rng.Next(-5, 6) / 100f;
                                accuracy = GenerateStat(luck, 90, 100);

                                luck += rng.Next(-5, 6) / 100f;
                                crit = GenerateStat(luck, 90, 110) / 25f;
                            }
                            else
                            {
                                luck += rng.Next(-5, 6) / 100f;
                                accuracy = GenerateStat(luck, 70, 130);

                                luck += rng.Next(-5, 6) / 100f;
                                crit = GenerateStat(luck, 30, 150) / 10f;
                            }

                            luck += rng.Next(-5, 6) / 100f;
                            power = GenerateStat(luck, 70, 130) / 100f * basePower;

                            break;
                        }
                    }
                }
                else
                {
                    if (randomCase <= 8)
                    {
                        switch (order)
                        {
                            case 0:
                                {
                                    luck += rng.Next(0, 16) / 100f;
                                    power = GenerateStat(luck, 70, 130) / 100f * basePower;

                                    if (magic)
                                    {
                                        luck += rng.Next(0, 16) / 100f;
                                        accuracy = GenerateStat(luck, 90, 100);

                                        luck += rng.Next(0, 16) / 100f;
                                        crit = GenerateStat(luck, 90, 110) / 25f;
                                    }
                                    else
                                    {
                                        luck += rng.Next(0, 16) / 100f;
                                        accuracy = GenerateStat(luck, 70, 130);

                                        luck += rng.Next(0, 16) / 100f;
                                        crit = GenerateStat(luck, 30, 150) / 10f;
                                    }

                                    break;
                                }
                            case 1:
                                {
                                    luck += rng.Next(0, 16) / 100f;
                                    power = GenerateStat(luck, 70, 130) / 100f * basePower;

                                    if (magic)
                                    {
                                        luck += rng.Next(0, 16) / 100f;
                                        crit = GenerateStat(luck, 90, 110) / 25f;

                                        luck += rng.Next(0, 16) / 100f;
                                        accuracy = GenerateStat(luck, 90, 100);
                                    }
                                    else
                                    {
                                        luck += rng.Next(0, 16) / 100f;
                                        crit = GenerateStat(luck, 30, 150) / 10f;

                                        luck += rng.Next(0, 16) / 100f;
                                        accuracy = GenerateStat(luck, 70, 130);
                                    }

                                    break;
                                }
                            case 2:
                                {
                                    if (magic)
                                    {
                                        luck += rng.Next(0, 16) / 100f;
                                        crit = GenerateStat(luck, 90, 110) / 25f;

                                        luck += rng.Next(0, 16) / 100f;
                                        power = GenerateStat(luck, 70, 130) / 100f * basePower;

                                        luck += rng.Next(0, 16) / 100f;
                                        accuracy = GenerateStat(luck, 90, 100);
                                    }
                                    else
                                    {
                                        luck += rng.Next(0, 16) / 100f;
                                        crit = GenerateStat(luck, 30, 150) / 10f;

                                        luck += rng.Next(0, 16) / 100f;
                                        power = GenerateStat(luck, 70, 130) / 100f * basePower;

                                        luck += rng.Next(0, 16) / 100f;
                                        accuracy = GenerateStat(luck, 70, 130);
                                    }

                                    break;
                                }
                            case 3:
                                {
                                    if (magic)
                                    {
                                        luck += rng.Next(0, 16) / 100f;
                                        accuracy = GenerateStat(luck, 90, 100);

                                        luck += rng.Next(0, 16) / 100f;
                                        crit = GenerateStat(luck, 90, 110) / 25f;
                                    }
                                    else
                                    {
                                        luck += rng.Next(0, 16) / 100f;
                                        accuracy = GenerateStat(luck, 70, 130);

                                        luck += rng.Next(0, 16) / 100f;
                                        crit = GenerateStat(luck, 30, 150) / 10f;
                                    }

                                    luck += rng.Next(0, 16) / 100f;
                                    power = GenerateStat(luck, 70, 130) / 100f * basePower;

                                    break;
                                }
                        }
                    }
                    else
                    {
                        if (randomCase <= 10)
                        {
                            switch (order)
                            {
                                case 0:
                                    {
                                        luck += rng.Next(-5, 1) / 100f;
                                        power = GenerateStat(luck, 70, 130) / 100f * basePower;

                                        if (magic)
                                        {
                                            luck += rng.Next(-5, 1) / 100f;
                                            accuracy = GenerateStat(luck, 90, 100);

                                            luck += rng.Next(-5, 1) / 100f;
                                            crit = GenerateStat(luck, 90, 110) / 25f;
                                        }
                                        else
                                        {
                                            luck += rng.Next(-5, 1) / 100f;
                                            accuracy = GenerateStat(luck, 70, 130);

                                            luck += rng.Next(-5, 1) / 100f;
                                            crit = GenerateStat(luck, 30, 150) / 10f;
                                        }

                                        break;
                                    }
                                case 1:
                                    {
                                        luck += rng.Next(-5, 1) / 100f;
                                        power = GenerateStat(luck, 70, 130) / 100f * basePower;

                                        if (magic)
                                        {
                                            luck += rng.Next(-5, 1) / 100f;
                                            crit = GenerateStat(luck, 90, 110) / 25f;

                                            luck += rng.Next(-5, 1) / 100f;
                                            accuracy = GenerateStat(luck, 90, 100);
                                        }
                                        else
                                        {
                                            luck += rng.Next(-5, 1) / 100f;
                                            crit = GenerateStat(luck, 30, 150) / 10f;

                                            luck += rng.Next(-5, 1) / 100f;
                                            accuracy = GenerateStat(luck, 70, 130);
                                        }

                                        break;
                                    }
                                case 2:
                                    {
                                        if (magic)
                                        {
                                            luck += rng.Next(-5, 1) / 100f;
                                            crit = GenerateStat(luck, 90, 110) / 25f;

                                            luck += rng.Next(-5, 1) / 100f;
                                            power = GenerateStat(luck, 70, 130) / 100f * basePower;

                                            luck += rng.Next(-5, 1) / 100f;
                                            accuracy = GenerateStat(luck, 90, 100);
                                        }
                                        else
                                        {
                                            luck += rng.Next(-5, 1) / 100f;
                                            crit = GenerateStat(luck, 30, 150) / 10f;

                                            luck += rng.Next(-5, 1) / 100f;
                                            power = GenerateStat(luck, 70, 130) / 100f * basePower;

                                            luck += rng.Next(-5, 1) / 100f;
                                            accuracy = GenerateStat(luck, 70, 130);
                                        }

                                        break;
                                    }
                                case 3:
                                    {
                                        if (magic)
                                        {
                                            luck += rng.Next(-5, 1) / 100f;
                                            accuracy = GenerateStat(luck, 90, 100);

                                            luck += rng.Next(-5, 1) / 100f;
                                            crit = GenerateStat(luck, 90, 110) / 25f;
                                        }
                                        else
                                        {
                                            luck += rng.Next(-5, 1) / 100f;
                                            accuracy = GenerateStat(luck, 70, 130);

                                            luck += rng.Next(-5, 1) / 100f;
                                            crit = GenerateStat(luck, 30, 150) / 10f;
                                        }

                                        luck += rng.Next(-5, 1) / 100f;
                                        power = GenerateStat(luck, 70, 130) / 100f * basePower;

                                        break;
                                    }
                            }
                        }
                        else
                        {
                            switch (order)
                            {
                                case 0:
                                    {
                                        luck += rng.Next(-15, 16) / 100f;
                                        power = GenerateStat(luck, 55, 145) / 100f * basePower;

                                        if (magic)
                                        {
                                            luck += rng.Next(-15, 16) / 100f;
                                            accuracy = GenerateStat(luck, 80, 110);

                                            luck += rng.Next(-15, 16) / 100f;
                                            crit = GenerateStat(luck, 75, 125) / 25f;
                                        }
                                        else
                                        {
                                            luck += rng.Next(-15, 16) / 100f;
                                            accuracy = GenerateStat(luck, 60, 140);

                                            luck += rng.Next(-15, 16) / 100f;
                                            crit = GenerateStat(luck, 0, 200) / 10f;
                                        }

                                        break;
                                    }
                                case 1:
                                    {
                                        luck += rng.Next(-15, 16) / 100f;
                                        power = GenerateStat(luck, 55, 145) / 100f * basePower;

                                        if (magic)
                                        {
                                            luck += rng.Next(-15, 16) / 100f;
                                            crit = GenerateStat(luck, 75, 125) / 25f;

                                            luck += rng.Next(-15, 16) / 100f;
                                            accuracy = GenerateStat(luck, 80, 110);
                                        }
                                        else
                                        {
                                            luck += rng.Next(-15, 16) / 100f;
                                            crit = GenerateStat(luck, 0, 200) / 10f;

                                            luck += rng.Next(-15, 16) / 100f;
                                            accuracy = GenerateStat(luck, 60, 140);
                                        }

                                        break;
                                    }
                                case 2:
                                    {
                                        if (magic)
                                        {
                                            luck += rng.Next(-15, 16) / 100f;
                                            crit = GenerateStat(luck, 75, 125) / 25f;

                                            luck += rng.Next(-15, 16) / 100f;
                                            power = GenerateStat(luck, 55, 145) / 100f * basePower;

                                            luck += rng.Next(-15, 16) / 100f;
                                            accuracy = GenerateStat(luck, 80, 110);
                                        }
                                        else
                                        {
                                            luck += rng.Next(-15, 16) / 100f;
                                            crit = GenerateStat(luck, 0, 200) / 10f;

                                            luck += rng.Next(-15, 16) / 100f;
                                            power = GenerateStat(luck, 55, 145) / 100f * basePower;

                                            luck += rng.Next(-15, 16) / 100f;
                                            accuracy = GenerateStat(luck, 60, 140);
                                        }

                                        break;
                                    }
                                case 3:
                                    {
                                        if (magic)
                                        {
                                            luck += rng.Next(-15, 16) / 100f;
                                            accuracy = GenerateStat(luck, 80, 110);

                                            luck += rng.Next(-15, 16) / 100f;
                                            crit = GenerateStat(luck, 75, 125) / 25f;
                                        }
                                        else
                                        {
                                            luck += rng.Next(-15, 16) / 100f;
                                            accuracy = GenerateStat(luck, 60, 140);

                                            luck += rng.Next(-15, 16) / 100f;
                                            crit = GenerateStat(luck, 0, 200) / 10f;
                                        }

                                        luck += rng.Next(-15, 16) / 100f;
                                        power = GenerateStat(luck, 55, 145) / 100f * basePower;

                                        break;
                                    }
                            }
                        }
                    }
                }

                if (!magic) power *= 1.2f;

                if (magic)
                {
                    manaCost = power / 10f * rng.Next(90, 110) / 10f * (1 + crit / 100f) * (accuracy / 100f);
                }
                else
                {
                    healthCost = power / 10f * rng.Next(90, 110) / 10f * (1 + crit / 100f) * (accuracy / 100f);
                }

                if (accuracy < 25) accuracy = 25f;
                if (crit < 0) crit = 0;
                if (power < 1) power = 1;

                AbilityAttack attack = new AbilityAttack(type, (int)Math.Round(power), (int)Math.Round(accuracy), (int)Math.Round(crit));

                totalAccuracy += accuracy;
                totalCrit += crit;
                totalHealthCost += healthCost;
                totalManaCost += manaCost;

                attacks[i] = attack;
            }

            totalHealthCost *= (float) Math.Pow(attackCount, 0.42);
            totalManaCost *= (float)Math.Pow(attackCount, 0.42);

            AbilityInfo info = new AbilityInfo();
            info.Attacks = attacks;
            info.HealthCost = (int)totalHealthCost;
            info.ManaCost = (int)totalManaCost;
            info.AverageAccuracy = (int)Math.Round(totalAccuracy / attackCount);
            info.AverageCritChance = (int)Math.Round(totalCrit / attackCount);
            GenerateSkillDescriptions(info);

            return info;
        }
    }
}