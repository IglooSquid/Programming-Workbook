using System;
using System.Collections.Generic;
using EnemyWaves;

namespace EnemyWaves
{
    public class Enemy
    {
        private string name;
        private int health;

        public string Name {get; set;}
        public int Health {get; set;}

        public Enemy (string name, int health)
        {
            this.Name = name;
            this.Health = health;
        }
    }
}