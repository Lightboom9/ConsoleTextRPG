using System;

namespace SharpLabProject.Skills
{
    public struct AbilityAttack
    {
        public DamageType type;
        public int power;
        public int accuracy;
        public int crit;

        public int GetLowerDamage(float multStat)
        {
            return (int) Math.Round(Math.Sqrt(multStat) * power * 0.85f);
        }
        public int GetHigherDamage(float multStat)
        {
            return (int) Math.Round(Math.Sqrt(multStat) * power * 1.15f);
        }
        public int GetDamage(float multStat)
        {
            Random rng = new Random();
            return rng.Next(GetLowerDamage(multStat), GetHigherDamage(multStat) + 1);
        }

        public AbilityAttack(DamageType type, int power, int accuracy, int crit)
        {
            this.type = type;
            this.power = power;
            this.accuracy = accuracy;
            this.crit = crit;
        }
    }
}