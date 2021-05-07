using System;
using System.Collections.Generic;
using ConsoleTextRPG.ConsoleRendering;
using ConsoleTextRPG.Items;
using ConsoleTextRPG.Skills;

namespace ConsoleTextRPG.Characters
{
    public class Player : Character
    {
        private int _baseEndurance;
        private int _baseWisdom;

        public int BaseEndurance
        {
            get => _baseEndurance;
            set
            {
                int diff = value - _baseEndurance;
                _baseEndurance = value;
                MaxHealth = _baseEndurance * 10;
            }
        }
        public int BaseIntelligence { get; protected set; }
        public int BaseWisdom
        {
            get => _baseWisdom;
            set
            {
                int diff = value - _baseWisdom;
                _baseWisdom = value;
                MaxMana = _baseWisdom * 10;
            }
        }
        public int BaseAgility { get; protected set; }
        public int BaseStrength { get; protected set; }
        public int BaseWits { get; protected set; }

        public int Endurance { get; protected set; }
        public int Intelligence { get; protected set; }
        public int Wisdom { get; protected set; }
        public int Agility { get; protected set; }
        public int Strength { get; protected set; }
        public int Wits { get; protected set; }

        public int NextSelectedSkillToUse { get; set; } = -1;

        private Character _lastTarget = null;

        public List<Item> Items { get; private set; }

        /// <summary>
        /// Creates a player with certain stats.
        /// </summary>
        /// <param name="strength">Determines physical attacks strength.</param>
        /// <param name="intelligence">Determines magical attacks strength.</param>
        /// <param name="agility">Determines physical attacks defense.</param>
        /// <param name="endurance">Determines max health.</param>
        /// <param name="wisdom">Determines magical attacks defense and max mana.</param>
        /// <param name="wits">Determines initiative.</param>
        public Player(int strength, int intelligence, int agility, int endurance, int wisdom, int wits) : base(endurance * 10, wisdom * 10, strength, intelligence, wits, agility, endurance, endurance, endurance, wisdom, wisdom, wisdom)
        {
            PlayerControlled = true;

            BaseStrength = strength;
            BaseIntelligence = intelligence;
            BaseAgility = agility;
            BaseEndurance = endurance;
            BaseWisdom = wisdom;
            BaseWits = wits;
        }

        public override void StartTurn(Character[] targets)
        {
            PhysicalPower = Strength;
            MagicalPower = Intelligence;
        }

        public override void EndTurn()
        {
            if (_lastTarget == null) return;

            Rendering.LockInput();
            Rendering.LockRendering();

            Console.Clear();

            string damageInfo = _lastTarget.GetLastReceivedDamageInfo();
            if (damageInfo != null)
            {
                Console.WriteLine("Enemy receives " + damageInfo + " damage.");
            }
            else
            {
                Console.WriteLine("Enemy doesn't receive any damage.");
            }

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey(true);

            Rendering.UnlockRendering();
            Rendering.Rerender(true);
            Rendering.UnlockInput();

            base.EndTurn();
        }

        public override void EnterFight()
        {
            base.EnterFight();

            Strength = BaseStrength;
            Agility = BaseAgility;
            Intelligence = BaseIntelligence;
            Endurance = BaseEndurance;
            Wisdom = BaseWisdom;
            Wits = BaseWits;
        }

        public void UseSelectedSkill(Character target)
        {
            if (NextSelectedSkillToUse == -1)
            {
                throw new ArgumentException("First, skill to use must be selected.");
            }

            UseSkill(Skills[NextSelectedSkillToUse], target);

            NextSelectedSkillToUse = -1;

            _lastTarget = target;

            EndTurn();
        }

        public string GetDescription(AbilityInfo info)
        {
            string desc = info.Description;

            for (int i = 0; i < info.Attacks.Length; i++)
            {
                string oldStr = $"[Atk{i+1}]";
                int multStat = (int) info.Attacks[i].type < 3 ? BasePhysicalPower : BaseMagicalPower;
                string newStr = $"{info.Attacks[i].GetLowerDamage(multStat)}-{info.Attacks[i].GetHigherDamage(multStat)}";

                desc = desc.Replace(oldStr, newStr);
            }

            return desc;
        }

        public int GetAverageLevel()
        {
            return (int) Math.Round((BaseStrength + BaseAgility + BaseIntelligence + BaseEndurance + BaseWisdom + BaseWits) / 6f);
        }

        public void FullRevive()
        {
            Health = MaxHealth;
            Mana = MaxMana;
            _alive = true;
        }

        public void Restore(int health, int mana)
        {
            Health += health;
            Mana += mana;
        }

        public void RaiseStats(int strength, int agility, int intelligence, int endurance, int wisdom, int wits)
        {
            BaseStrength += strength;
            BaseAgility += agility;
            BaseIntelligence += intelligence;
            BaseEndurance += endurance;
            BaseWisdom += wisdom;
            BaseWits += wits;
        }
    }
}