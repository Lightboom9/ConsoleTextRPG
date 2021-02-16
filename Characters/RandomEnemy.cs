namespace ConsoleTextRPG.Characters
{
    public class RandomEnemy : Character
    {
        public RandomEnemy(int health, int mana, int physPower, int magePower, int initiative, int bluntResist, int cutResist, int piercingResist, int fireResist, int iceResist, int airResist) : base(health, mana, physPower, magePower, initiative, bluntResist, cutResist, piercingResist, fireResist, iceResist, airResist)
        {
            
        }

        public override void Act(Character[] targets)
        {

        }
    }
}