using System;
using System.Collections.Generic;
using MiningSimulator;

namespace MiningSimulator
{
    public class Node
    {
        private string name;
        private int charges;

        public string Name {get; set;}
        public int Charges {get; set;}

        public Node (string name, int charges)
        {
            this.Name = name;
            this.Charges = charges;
        }
    }
}