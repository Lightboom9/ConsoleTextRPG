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

        public void Act(IUnit target)
        {

        }

        public void ReceiveAttack(AbilityAttack attack)
        {

        }
    }
}