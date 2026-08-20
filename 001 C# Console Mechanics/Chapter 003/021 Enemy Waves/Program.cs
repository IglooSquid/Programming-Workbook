using System;
using System.Collections.Generic;
using EnemyWaves;

namespace EnemyWaves
{
    public static class Program
    {
        static int wavesQuantity = 5;
        static float healthModifier = 1.15f;
        static int damage = 25;
        static int damageVariance = 10;
        static int maxEnemiesPerWave = 3;
        static int currentWave = 0;

        static Dictionary<string, int> enemyTypes = new Dictionary<string, int>();




        public static void Main(String[] args)
        {
            PopulateLists();
            while (currentWave < wavesQuantity - 1)
            {
                SpawnEnemyWave();
            }
            SpawnBoss();
            Console.WriteLine($"Be seeing you.");
        }

        public static void SpawnBoss()
        {
            currentWave++;
            Console.WriteLine($"Wave {currentWave}.");
            Enemy Boss = new Enemy("Pit fiend", 1500);
            Console.WriteLine($"Spawned 1 x {Boss.Name} ({Boss.Health} HP).");
            Console.ReadKey(true);            
        }

        public static void SpawnEnemyWave()
        {
            currentWave++;
            Console.WriteLine($"Wave {currentWave}.");
            var random = new Random();
            int number = random.Next(1, maxEnemiesPerWave+1);

            var random2 = new Random();
            int number2 = random2.Next(enemyTypes.Count);
            string enemy = enemyTypes.ElementAt(number2).Key;
            int health = enemyTypes.ElementAt(number2).Value;

            Enemy Enemy1 = new Enemy(enemy + " 1", (int)(health * (healthModifier*currentWave)));
            if (number > 1)
            {
                Enemy Enemy2 = new Enemy(enemy + " 2", (int)(health * (healthModifier*currentWave)));
            }
            if (number > 2)
            {
                Enemy Enemy3 = new Enemy(enemy + " 3", (int)(health * (healthModifier*currentWave)));
            }

            Console.WriteLine($"Spawned {number} x {enemy} ({Enemy1.Health} HP).");
            Console.ReadKey(true);
        }

        public static void PopulateLists()
        {
            enemyTypes.Add("Goblin", 75);
            enemyTypes.Add("Orc", 100);
            enemyTypes.Add("Kobold", 50);
        }
    }
}