using System;
using System.Collections.Generic;
using ConsoleTextRPG.Skills;

namespace ConsoleTextRPG.Characters
{
    public abstract class Character
    {
        public bool PlayerControlled { get; protected set; } = false;

        protected int _health;
        protected bool _alive;

        public int MaxHealth { get; protected set; }
        public int MaxMana { get; protected set; }

        public int Health
        {
            get => _health;
            protected set
            {
                _health = value;
                if (_health <= 0)
                {
                    _health = 0;
                    _alive = false;
                }
            }
        }
        public int Mana { get; protected set; }

        public int BasePhysicalPower { get; protected set; }
        public int BaseMagicalPower { get; protected set; }
        public int BaseInitiative { get; protected set;  }

        public int BaseBluntResist { get; protected set; }
        public int BaseCutResist { get; protected set; }
        public int BasePiercingResist { get; protected set; }
        public int BaseFireResist { get; protected set; }
        public int BaseIceResist { get; protected set; }
        public int BaseAirResist { get; protected set; }

        public int PhysicalPower { get; protected set; }
        public int MagicalPower { get; protected set; }
        public int Initiative { get; protected set; }

        public int BluntResist { get; protected set; }
        public int CutResist { get; protected set; }
        public int PiercingResist { get; protected set; }
        public int FireResist { get; protected set; }
        public int IceResist { get; protected set; }
        public int AirResist { get; protected set; }

        public List<AbilityInfo> Skills { get; } = new List<AbilityInfo>();

        protected Character(int health, int mana, int physPower, int magePower, int initiative, int bluntResist, int cutResist, int piercingResist, int fireResist, int iceResist, int airResist)
        {
            Health = MaxHealth = health;
            Mana = MaxMana = mana;

            BasePhysicalPower = physPower;
            BaseMagicalPower = magePower;
            BaseInitiative = initiative;

            BaseBluntResist = bluntResist;
            BaseCutResist = cutResist;
            BasePiercingResist = piercingResist;
            BaseFireResist = fireResist;
            BaseIceResist = iceResist;
            BaseAirResist = airResist;
        }

        public virtual void EnterFight()
        {
            PhysicalPower = BasePhysicalPower;
            MagicalPower = BaseMagicalPower;
            Initiative = BaseInitiative;

            BluntResist = BaseBluntResist;
            CutResist = BaseCutResist;
            PiercingResist = BasePiercingResist;
            FireResist = BaseFireResist;
            IceResist = BaseIceResist;
            AirResist = BaseAirResist;
        }

        public abstract void Act(Character[] targets);

        public virtual void ReceiveAttack(Character attacker, AbilityAttack attack)
        {
            switch (attack.type)
            {
                case DamageType.Pure:
                {
                    Health -= attack.GetDamage(attacker.MagicalPower);

                    break;
                }
                case DamageType.Fire:
                {
                    Health -= (int)Math.Round(attack.GetDamage(attacker.MagicalPower) / Math.Sqrt(FireResist));

                    break;
                }
                case DamageType.Ice:
                {
                    Health -= (int)Math.Round(attack.GetDamage(attacker.MagicalPower) / Math.Sqrt(IceResist));

                    break;
                }
                case DamageType.Air:
                {
                    Health -= (int)Math.Round(attack.GetDamage(attacker.MagicalPower) / Math.Sqrt(AirResist));

                    break;
                }
                case DamageType.Blunt:
                {
                    Health -= (int)Math.Round(attack.GetDamage(attacker.PhysicalPower) / Math.Sqrt(BluntResist));

                    break;
                }
                case DamageType.Cut:
                {
                    Health -= (int)Math.Round(attack.GetDamage(attacker.PhysicalPower) / Math.Sqrt(CutResist));

                    break;
                }
                case DamageType.Piercing:
                {
                    Health -= (int)Math.Round(attack.GetDamage(attacker.PhysicalPower) / Math.Sqrt(PiercingResist));

                    break;
                }
            }
        }
    }
}