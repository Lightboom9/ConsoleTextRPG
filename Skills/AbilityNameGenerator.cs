using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpLabProject.Skills
{
    public static class AbilityNameGenerator
    {
        private static string GetBluntMainName()
        {
            Random rng = new Random();
            int casesCount = 4;

            switch (rng.Next(0, casesCount))
            {
                case 0: return "Club";
                case 1: return "Hammer";
                case 2: return "Fist";
                case 3: return "Mace";
                default: return null;
            }
        }
        private static string GetCutMainName()
        {
            Random rng = new Random();
            int casesCount = 4;

            switch (rng.Next(0, casesCount))
            {
                case 0: return "Cut";
                case 1: return "Knife";
                case 2: return "Blade";
                case 3: return "Sword";
                default: return null;
            }
        }
        private static string GetPiercingMainName()
        {
            Random rng = new Random();
            int casesCount = 4;

            switch (rng.Next(0, casesCount))
            {
                case 0: return "Spear";
                case 1: return "Arrow";
                case 2: return "Spire";
                case 3: return "Spike";
                default: return null;
            }
        }
        private static string GetDoublePhysicalMainName()
        {
            Random rng = new Random();
            int casesCount = 4;

            switch (rng.Next(0, casesCount))
            {
                case 0: return "Rampage";
                case 1: return "Strikes";
                case 2: return "Massacre";
                case 3: return "Whirlwind";
                default: return null;
            }
        }
        private static string GetFireMainName()
        {
            Random rng = new Random();
            int casesCount = 6;

            switch (rng.Next(0, casesCount))
            {
                case 0: return "Burst";
                case 1: return "Flare";
                case 2: return "Flame";
                case 3: return "Fireball";
                case 4: return "Pyre";
                case 5: return "Burn";
                default: return null;
            }
        }
        private static string GetIceMainName()
        {
            Random rng = new Random();
            int casesCount = 6;

            switch (rng.Next(0, casesCount))
            {
                case 0: return "Shard";
                case 1: return "Freeze";
                case 2: return "Algidity";
                case 3: return "Crystal";
                case 4: return "Splinter";
                case 5: return "Fraction";
                default: return null;
            }
        }
        private static string GetAirMainName()
        {
            Random rng = new Random();
            int casesCount = 6;

            switch (rng.Next(0, casesCount))
            {
                case 0: return "Spark";
                case 1: return "Lightning";
                case 2: return "Gale";
                case 3: return "Storm";
                case 4: return "Hurricane";
                case 5: return "Tempest";
                default: return null;
            }
        }
        private static string GetPureMainName()
        {
            Random rng = new Random();
            int casesCount = 2;

            switch (rng.Next(0, casesCount))
            {
                case 0: return "Star";
                case 1: return "Line";
                default: return null;
            }
        }
        private static string GetDoubleMagicMainName()
        {
            Random rng = new Random();
            int casesCount = 6;

            switch (rng.Next(0, casesCount))
            {
                case 0: return "Miracle";
                case 1: return "Mastership";
                case 2: return "Art";
                case 3: return "Chaos";
                case 4: return "Nova";
                case 5: return "Outbreak";
                default: return null;
            }
        }

        private static string GetPhysicalBeaut()
        {
            Random rng = new Random();
            int casesCount = 10;

            switch (rng.Next(0, casesCount))
            {
                case 0: return "Bloody";
                case 1: return "Throwing";
                case 2: return "Flying";
                case 3: return "Destined";
                case 4: return "Cruel";
                case 5: return "Fierce";
                case 6: return "Savage";
                case 7: return "Brute's";
                case 8: return "Heroic";
                case 9: return "Swift";
                default: return null;
            }
        }
        private static string GetMagicalBeaut()
        {
            Random rng = new Random();
            int casesCount = 8;

            switch (rng.Next(0, casesCount))
            {
                case 0: return "Magnificent";
                case 1: return "Complex";
                case 2: return "Illusory";
                case 3: return "Violent";
                case 4: return "Keen";
                case 5: return "Faerie";
                case 6: return "Magician's";
                case 7: return "Theurgic";
                default: return null;
            }
        }

        private static string GetFireBeaut()
        {
            Random rng = new Random();
            int casesCount = 4;

            switch (rng.Next(0, casesCount))
            {
                case 0: return "Fiery";
                case 1: return "Burning";
                case 2: return "Explosive";
                case 3: return "Red";
                default: return null;
            }
        }
        private static string GetIceBeaut()
        {
            Random rng = new Random();
            int casesCount = 4;

            switch (rng.Next(0, casesCount))
            {
                case 0: return "Cold";
                case 1: return "Chilling";
                case 2: return "Winter";
                case 3: return "Blue";
                default: return null;
            }
        }
        private static string GetAirBeaut()
        {
            Random rng = new Random();
            int casesCount = 4;

            switch (rng.Next(0, casesCount))
            {
                case 0: return "Stormy";
                case 1: return "Windy";
                case 2: return "Wild";
                case 3: return "Green";
                default: return null;
            }
        }
        private static string GetPureBeaut()
        {
            Random rng = new Random();
            int casesCount = 2;

            switch (rng.Next(0, casesCount))
            {
                case 0: return "Astral";
                case 1: return "Almighty";
                default: return null;
            }
        }

        private static string[] _physicals = new[] {"Blunt", "Piercing", "Cut"};
        private static string[] _magicals = new[] { "Fire", "Ice", "Air", "Pure" };

        public static string GenerateAttackName(string name1, string name2)
        {
            string name = "";

            if (name2 != null)
            {
                bool physName1 = _physicals.Contains(name1);
                bool physName2 = _physicals.Contains(name2);

                if ((physName1 && !physName2) || (physName2 && !physName1))
                {
                    if (physName1 && !physName2)
                    {
                        switch (name2)
                        {
                            case "Fire":
                            {
                                name += GetFireBeaut();

                                break;
                            }
                            case "Air":
                            {
                                name += GetAirBeaut();

                                break;
                            }
                            case "Ice":
                            {
                                name += GetIceBeaut();

                                break;
                            }
                            case "Pure":
                            {
                                name += GetPureBeaut();

                                break;
                            }
                        }

                        name += " ";

                        switch (name1)
                        {
                            case "Blunt":
                            {
                                name += GetBluntMainName().ToLower();

                                break;
                            }
                            case "Cut":
                            {
                                name += GetCutMainName().ToLower();

                                break;
                            }
                            case "Piercing":
                            {
                                name += GetPiercingMainName().ToLower();

                                break;
                            }
                        }
                    }
                    else
                    {
                        switch (name1)
                        {
                            case "Fire":
                            {
                                name += GetFireBeaut();

                                break;
                            }
                            case "Air":
                            {
                                name += GetAirBeaut();

                                break;
                            }
                            case "Ice":
                            {
                                name += GetIceBeaut();

                                break;
                            }
                            case "Pure":
                            {
                                name += GetPureBeaut();

                                break;
                            }
                        }

                        name += " ";

                        switch (name2)
                        {
                            case "Blunt":
                            {
                                name += GetBluntMainName().ToLower();

                                break;
                            }
                            case "Cut":
                            {
                                name += GetCutMainName().ToLower();

                                break;
                            }
                            case "Piercing":
                            {
                                name += GetPiercingMainName().ToLower();

                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (physName1 && physName2)
                    {
                        name = GetPhysicalBeaut() + " " + GetDoublePhysicalMainName().ToLower();
                    }
                    else
                    {
                        name = GetMagicalBeaut() + " " + GetDoubleMagicMainName().ToLower();
                    }
                }
            }
            else
            {
                bool physical = _physicals.Contains(name1);

                if (physical)
                {
                    name += GetPhysicalBeaut() + " ";

                    switch (name1)
                    {
                        case "Blunt":
                        {
                            name += GetBluntMainName().ToLower();

                            break;
                        }
                        case "Cut":
                        {
                            name += GetCutMainName().ToLower();

                            break;
                        }
                        case "Piercing":
                        {
                            name += GetPiercingMainName().ToLower();

                            break;
                        }
                    }
                }
                else
                {
                    name += GetMagicalBeaut() + " ";

                    switch (name1)
                    {
                        case "Fire":
                        {
                            name += GetFireBeaut().ToLower();

                            break;
                        }
                        case "Air":
                        {
                            name += GetAirBeaut().ToLower();

                            break;
                        }
                        case "Ice":
                        {
                            name += GetIceBeaut().ToLower();

                            break;
                        }
                        case "Pure":
                        {
                            name += GetPureBeaut().ToLower();

                            break;
                        }
                    }
                }
            }

            return name;
        }

        public static string GenerateSupportName()
        {
            return null;
        }
    }
}