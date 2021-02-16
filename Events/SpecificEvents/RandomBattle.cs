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

            int health = rng.Next(low, high) * (int)Math.Round(rng.Next(95, 106) / 10f);
            int mana = rng.Next(low, high) * (int)Math.Round(rng.Next(95, 106) / 10f);
            int physPower = rng.Next(low, high) * (int)Math.Round(rng.Next(95, 106) / 100f);
            int magePower = rng.Next(low, high) * (int)Math.Round(rng.Next(95, 106) / 100f);
            int init = rng.Next(low, high) * (int)Math.Round(rng.Next(95, 106) / 100f);
            int bluntRes = rng.Next(low, high) * (int)Math.Round(rng.Next(95, 106) / 100f);
            int cutRes = rng.Next(low, high) * (int)Math.Round(rng.Next(95, 106) / 100f);
            int piercingRes = rng.Next(low, high) * (int)Math.Round(rng.Next(95, 106) / 100f);
            int fireRes = rng.Next(low, high) * (int)Math.Round(rng.Next(95, 106) / 100f);
            int iceRes = rng.Next(low, high) * (int)Math.Round(rng.Next(95, 106) / 100f);
            int airRes = rng.Next(low, high) * (int)Math.Round(rng.Next(95, 106) / 100f);

            RandomEnemy enemy = new RandomEnemy(health, mana, physPower, magePower, init, bluntRes, cutRes, piercingRes, fireRes, iceRes, airRes);

            int skillCount = rng.Next(0, 4);
            for (int i = 0; i < skillCount; i++) enemy.Skills.Add(AbilityInfo.Generate(enemyLevel));

            battle.Enemy = enemy;

            return battle;
        }
    }
}