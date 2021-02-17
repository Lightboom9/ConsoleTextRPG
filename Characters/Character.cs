using System;
using System.Collections.Generic;
using System.Text;
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

        public bool IsAlive => _alive;

        protected List<string> _lastReceivedDamagesInfo = new List<string>();

        public string GetLastReceivedDamageInfo()
        {
            if (_lastReceivedDamagesInfo.Count == 0) return null;

            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < _lastReceivedDamagesInfo.Count; i++)
            {
                var str = _lastReceivedDamagesInfo[i];
                if (i > 0) sb.Append(", ");
                sb.Append(str);
            }

            _lastReceivedDamagesInfo.Clear();

            return sb.ToString();
        }

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

        public Action OnTurnEnd { get; set; }

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

        public abstract void StartTurn(Character[] targets);

        public virtual void EndTurn()
        {
            OnTurnEnd?.Invoke();
        }

        public virtual void UseSkill(AbilityInfo skill, Character target)
        {
            if (skill.HealthCost >= Health) return;
            if (skill.ManaCost > Mana) return;

            foreach (var atk in skill.Attacks)
            {
                target.ReceiveAttack(this, atk);
            }

            Health -= skill.HealthCost;
            Mana -= skill.ManaCost;
        }

        public virtual void ReceiveAttack(Character attacker, AbilityAttack attack)
        {
            switch (attack.type)
            {
                case DamageType.Pure:
                {
                    int damage = attack.GetDamage(attacker.MagicalPower);
                    Health -= damage;

                    _lastReceivedDamagesInfo.Add(damage + " pure");

                    break;
                }
                case DamageType.Fire:
                {
                    int damage = (int)Math.Round(attack.GetDamage(attacker.MagicalPower) / Math.Sqrt(FireResist));
                    Health -= damage;

                    _lastReceivedDamagesInfo.Add(damage + " fire");

                    break;
                }
                case DamageType.Ice:
                {
                    int damage = (int)Math.Round(attack.GetDamage(attacker.MagicalPower) / Math.Sqrt(IceResist));
                    Health -= damage;

                    _lastReceivedDamagesInfo.Add(damage + " ice");

                    break;
                }
                case DamageType.Air:
                {
                    int damage = (int)Math.Round(attack.GetDamage(attacker.MagicalPower) / Math.Sqrt(AirResist));
                    Health -= damage;

                    _lastReceivedDamagesInfo.Add(damage + " air");

                    break;
                }
                case DamageType.Blunt:
                {
                    int damage = (int)Math.Round(attack.GetDamage(attacker.PhysicalPower) / Math.Sqrt(BluntResist));
                    Health -= damage;

                    _lastReceivedDamagesInfo.Add(damage + " blunt");

                    break;
                }
                case DamageType.Cut:
                {
                    int damage = (int)Math.Round(attack.GetDamage(attacker.PhysicalPower) / Math.Sqrt(CutResist));
                    Health -= damage;

                    _lastReceivedDamagesInfo.Add(damage + " cut");

                    break;
                }
                case DamageType.Piercing:
                {
                    int damage = (int)Math.Round(attack.GetDamage(attacker.PhysicalPower) / Math.Sqrt(PiercingResist));
                    Health -= damage;

                    _lastReceivedDamagesInfo.Add(damage + " piercing");

                    break;
                }
            }
        }
    }
}