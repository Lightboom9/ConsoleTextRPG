using System;
using System.Collections.Generic;
using SharpLabProject.Skills;

namespace SharpLabProject.Characters
{
    public class Player : IUnit
    {
        private int _health;
        private bool _alive;

        public int MaxHealth { get; private set; }
        public int MaxMana { get; private set; }

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                if (_health <= 0)
                {
                    _health = 0;
                    _alive = false;
                }
            }
        }
        public int Mana { get; private set; }

        public int BaseEndurance { get; private set; }
        public int BaseIntelligence { get; private set; }
        public int BaseWisdom { get; private set; }
        public int BaseAgility { get; private set; }
        public int BaseStrength { get; private set; }
        public int BaseWits { get; private set; }

        public int BaseBluntResist { get; private set; }
        public int BaseCutResist { get; private set; }
        public int BasePiercingResist { get; private set; }
        public int BaseFireResist { get; private set; }
        public int BaseIceResist { get; private set; }
        public int BaseAirResist { get; private set; }

        public int Endurance { get; private set; }
        public int Intelligence { get; private set; }
        public int Wisdom { get; private set; }
        public int Agility { get; private set; }
        public int Strength { get; private set; }
        public int Wits { get; private set; }

        public int BluntResist { get; private set; }
        public int CutResist { get; private set; }
        public int PiercingResist { get; private set; }
        public int FireResist { get; private set; }
        public int IceResist { get; private set; }
        public int AirResist { get; private set; }

        public List<AbilityInfo> Skills { get; } = new List<AbilityInfo>();

        /// <summary>
        /// Creates a player with certain stats.
        /// </summary>
        /// <param name="strength">Determines physical attacks strength.</param>
        /// <param name="intelligence">Determines magical attacks strength.</param>
        /// <param name="agility">Determines physical attacks defense.</param>
        /// <param name="endurance">Determines max health.</param>
        /// <param name="wisdom">Determines magical attacks defense and max mana.</param>
        /// <param name="wits">Determines initiative.</param>
        public Player(int strength, int intelligence, int agility, int endurance, int wisdom, int wits)
        {
            BaseStrength = strength;
            BaseIntelligence = intelligence;
            BaseAgility = agility;
            BaseEndurance = endurance;
            BaseWisdom = wisdom;
            BaseWits = wits;

            MaxHealth = Health = endurance * 10;
            MaxMana = Mana = wisdom * 10;
            BaseBluntResist = agility;
            BaseCutResist = agility;
            BasePiercingResist = agility;
            BaseFireResist = wisdom;
            BaseIceResist = wisdom;
            BaseAirResist = wisdom;
        }

        public int GetHealth() => _health;
        public int GetInitiative() => Wits;
        public bool IsAlive() => _alive;
        public int GetBluntResist() => BluntResist;
        public int GetCutResist() => CutResist;
        public int GetPiercingResist() => PiercingResist;
        public int GetFireResist() => FireResist;
        public int GetIceResist() => IceResist;
        public int GetAirResist() => AirResist;
        public float GetMagicMult() => Intelligence;
        public float GetPhysicalMult() => Strength;

        public void BeginFight()
        {
            Intelligence = BaseIntelligence;
            Strength = BaseStrength;
            Agility = BaseAgility;
            Endurance = BaseEndurance;
            Wits = BaseWits;
            Wisdom = BaseWisdom;

            BluntResist = BaseBluntResist;
            CutResist = BaseCutResist;
            PiercingResist = BasePiercingResist;
            FireResist = BaseFireResist;
            IceResist = BaseIceResist;
            AirResist = BaseAirResist;
        }

        public void Act(IUnit target)
        {

        }

        public void ReceiveAttack(IUnit attacker, AbilityAttack attack)
        {
            switch (attack.type)
            {
                case DamageType.Pure:
                {
                    Health -= attack.GetDamage(attacker.GetMagicMult());

                    break;
                }
                case DamageType.Fire:
                {
                    Health -= (int) Math.Round(attack.GetDamage(attacker.GetMagicMult()) / Math.Sqrt(FireResist));

                    break;
                }
                case DamageType.Ice:
                {
                    Health -= (int)Math.Round(attack.GetDamage(attacker.GetMagicMult()) / Math.Sqrt(IceResist));

                    break;
                }
                case DamageType.Air:
                {
                    Health -= (int)Math.Round(attack.GetDamage(attacker.GetMagicMult()) / Math.Sqrt(AirResist));

                    break;
                }
                case DamageType.Blunt:
                {
                    Health -= (int)Math.Round(attack.GetDamage(attacker.GetPhysicalMult()) / Math.Sqrt(BluntResist));

                    break;
                }
                case DamageType.Cut:
                {
                    Health -= (int)Math.Round(attack.GetDamage(attacker.GetPhysicalMult()) / Math.Sqrt(CutResist));

                    break;
                }
                case DamageType.Piercing:
                {
                    Health -= (int)Math.Round(attack.GetDamage(attacker.GetPhysicalMult()) / Math.Sqrt(PiercingResist));

                    break;
                }
            }
        }

        public string GetDescription(AbilityInfo info)
        {
            string desc = info.Description;

            for (int i = 0; i < info.Attacks.Length; i++)
            {
                string oldStr = $"[Atk{i+1}]";
                int multStat = (int) info.Attacks[i].type < 3 ? BaseStrength : BaseIntelligence;
                string newStr = $"{info.Attacks[i].GetLowerDamage(multStat)}-{info.Attacks[i].GetHigherDamage(multStat)}";

                desc = desc.Replace(oldStr, newStr);
            }

            return desc;
        }
    }
}