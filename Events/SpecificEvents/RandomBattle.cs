using System;
using ConsoleTextRPG.Characters;
using ConsoleTextRPG.Skills;

namespace ConsoleTextRPG.GameEvents
{
    public class RandomBattle : GameEvent
    {
        public Character Enemy { get; private set; }

        public static RandomBattle Generate(int enemyLevel)
        {
            Random rng = new Random();

            RandomBattle battle = new RandomBattle();

            int low = enemyLevel - (int)Math.Sqrt(enemyLevel);
            int high = enemyLevel + (int)Math.Sqrt(enemyLevel) + 1;

            int health = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 10f));
            int mana = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 10f));
            int physPower = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
            int magePower = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
            int init = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
            int bluntRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
            int cutRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
            int piercingRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
            int fireRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
            int iceRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
            int airRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));

            RandomEnemy enemy = new RandomEnemy(health, mana, physPower, magePower, init, bluntRes, cutRes, piercingRes, fireRes, iceRes, airRes);

            int skillCount = rng.Next(1, 4);
            for (int i = 0; i < skillCount; i++) enemy.Skills.Add(AbilityInfo.Generate(enemyLevel));

            battle.Enemy = enemy;

            return battle;
        }
    }
}