using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConsoleTextRPG.ConsoleRendering;
using ConsoleTextRPG.Skills;

namespace ConsoleTextRPG.Characters
{
    public class RandomEnemy : Character
    {
        public string[] Weaknesses { get; private set; }
        public string[] Strengths { get; private set; }

        protected RandomEnemy(int health, int mana, int physPower, int magePower, int initiative, int bluntResist, int cutResist, int piercingResist, int fireResist, int iceResist, int airResist) : base(health, mana, physPower, magePower, initiative, bluntResist, cutResist, piercingResist, fireResist, iceResist, airResist)
        {
            
        }

        public override void StartTurn(Character[] targets)
        {
            Rendering.LockInput();
            Rendering.LockRendering();

            Random rng = new Random();

            Console.Clear();
            Console.WriteLine("Enemy is thinking...");
            Thread.Sleep(rng.Next(250, 751));

            Character target = targets[rng.Next(0, targets.Length)];
            UseSkill(Skills[rng.Next(0, Skills.Count)], target);

            string lastDamage = target.GetLastReceivedDamageInfo();
            if (lastDamage != null)
            {
                Console.WriteLine("\nYou receive " + lastDamage + " damage.");
            }
            else
            {
                Console.WriteLine("\nYou don't receive any damage.");
            }

            Thread.Sleep(250);
            Console.WriteLine("\nPress any key to continue.");

            Console.ReadKey(true);

            EndTurn();
        }

        public override void EndTurn()
        {
            Rendering.UnlockRendering();
            Rendering.Rerender(true);
            Rendering.UnlockInput();

            base.EndTurn();
        }

        public static RandomEnemy Generate(int enemyLevel)
        {
            Random rng = new Random();

            int low = enemyLevel - (int)Math.Sqrt(enemyLevel);
            int high = enemyLevel + (int)Math.Sqrt(enemyLevel) + 1;

            int amountOfWeaks = rng.Next(1, 3);
            int amountOfStrongs = rng.Next(0, 4);
            int[] weaks = new int[amountOfWeaks];
            int[] strongs = new int[amountOfStrongs];
            for (int i = 0; i < amountOfWeaks; i++)
            {
                int index = rng.Next(0, 6);
                bool flag = false;
                for (int k = 0; k < i; k++)
                {
                    if (weaks[k] == index)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    i--;
                    continue;
                }

                weaks[i] = index;
            }
            for (int i = 0; i < amountOfStrongs; i++)
            {
                int index = rng.Next(0, 6);
                bool flag = false;
                for (int k = 0; k < i; k++)
                {
                    if (strongs[k] == index)
                    {
                        flag = true;
                        break;
                    }
                }
                for (int k = 0; k < amountOfWeaks; k++)
                {
                    if (weaks[k] == index)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    i--;
                    continue;
                }

                strongs[i] = index;
            }

            int health = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 10f));
            int mana = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 10f));
            int physPower = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
            int magePower = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
            int init = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
            int bluntRes = 0, cutRes = 0, piercingRes = 0, fireRes = 0, iceRes = 0, airRes = 0;

            List<string> descWeaks = new List<string>();
            List<string> descStrongs = new List<string>();

            if (weaks.Contains(0))
            {
                bluntRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f) * 0.5f);

                descWeaks.Add("blunt");
            }
            else
            {
                if (strongs.Contains(0))
                {
                    bluntRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f) * 2f);

                    descStrongs.Add("blunt");
                }
                else
                {
                    bluntRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
                }
            }
            if (weaks.Contains(1))
            {
                cutRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f) * 0.5f);

                descWeaks.Add("cut");
            }
            else
            {
                if (strongs.Contains(1))
                {
                    cutRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f) * 2f);

                    descStrongs.Add("cut");
                }
                else
                {
                    cutRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
                }
            }
            if (weaks.Contains(2))
            {
                piercingRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f) * 0.5f);

                descWeaks.Add("piercing");
            }
            else
            {
                if (strongs.Contains(2))
                {
                    piercingRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f) * 2f);

                    descStrongs.Add("piercing");
                }
                else
                {
                    piercingRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
                }
            }
            if (weaks.Contains(3))
            {
                fireRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f) * 0.5f);

                descWeaks.Add("fire");
            }
            else
            {
                if (strongs.Contains(3))
                {
                    fireRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f) * 2f);

                    descStrongs.Add("fire");
                }
                else
                {
                    fireRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
                }
            }
            if (weaks.Contains(4))
            {
                iceRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f) * 0.5f);

                descWeaks.Add("ice");
            }
            else
            {
                if (strongs.Contains(4))
                {
                    iceRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f) * 2f);

                    descStrongs.Add("ice");
                }
                else
                {
                    iceRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
                }
            }
            if (weaks.Contains(5))
            {
                airRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f) * 0.5f);

                descWeaks.Add("air");
            }
            else
            {
                if (strongs.Contains(5))
                {
                    airRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f) * 2f);

                    descStrongs.Add("air");
                }
                else
                {
                    airRes = (int)Math.Round(rng.Next(low, high) * (rng.Next(95, 106) / 100f));
                }
            }

            RandomEnemy enemy = new RandomEnemy(health, mana, physPower, magePower, init, bluntRes, cutRes, piercingRes, fireRes, iceRes, airRes);

            enemy.Strengths = descStrongs.ToArray();
            enemy.Weaknesses = descWeaks.ToArray();

            int skillCount = rng.Next(1, 4);
            for (int i = 0; i < skillCount; i++) enemy.Skills.Add(AbilityInfo.Generate(enemyLevel));

            return enemy;
        }
    }
}