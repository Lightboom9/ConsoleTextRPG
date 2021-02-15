using System;

namespace SharpLabProject.Skills
{
    public struct AbilityAttack
    {
        public DamageType type;
        public int power;
        public int accuracy;
        public int crit;
        public int multStat;

        public int GetLowerDamage()
        {
            return (int) (multStat / 10f * power * 0.85f);
        }
        public int GetHigherDamage()
        {
            return (int) (multStat / 10f * power * 1.15f);
        }
        public int GetDamage()
        {
            Random rng = new Random();

            return rng.Next(GetLowerDamage(), GetHigherDamage() + 1);
        }

        public AbilityAttack(DamageType type, int power, int multStat, int accuracy, int crit)
        {
            this.type = type;
            this.power = power;
            this.multStat = multStat;
            this.accuracy = accuracy;
            this.crit = crit;
        }
    }
}