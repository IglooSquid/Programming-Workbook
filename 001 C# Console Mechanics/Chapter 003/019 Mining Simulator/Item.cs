using System;
using System.Collections.Generic;
using MiningSimulator;

namespace MiningSimulator
{
    public class Item
    {
        private string name;
        private int rarity;

        public string Name {get; set;}
        public byte Rarity {get; set;}

        public Item (string name, byte rarity)
        {
            this.Name = name;
            this.Rarity = rarity;
        }
    }
}