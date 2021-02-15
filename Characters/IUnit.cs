using SharpLabProject.Skills;

namespace SharpLabProject.Characters
{
    public interface IUnit
    {
        public int GetHealth();
        public bool IsAlive();
        public int GetInitiative();

        public int GetBluntResist();
        public int GetCutResist();
        public int GetPiercingResist();
        public int GetFireResist();
        public int GetIceResist();
        public int GetAirResist();

        public void Act(IUnit opponent);
        public void ReceiveAttack(AbilityAttack attack);
    }
}