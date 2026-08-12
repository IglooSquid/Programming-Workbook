using System;
using System.Collections.Generic;


        string creature = "Crow";
        string location = "Glade";
        int hitPoints = 1;

        /**
        List<string> creatureList = new List<string>
        {
            "Rat",
            "Crow",
            "Fox",
            "Wolf",
            "Boar",
            "Raccoon",
            "Sparrow",
            "Frog",
            "Rabbit",
            "Gecko",
            "Horse"
        };
        **/

        List<string> locationList = new List<string>
        {
            "Glade",
            "Swamp",
            "Cave",
            "Field",
            "Shore"
        };

        Dictionary<string, int> creatureDictionary = new Dictionary<string, int>();

        creatureDictionary.Add("Rat", 1);
        creatureDictionary.Add("Crow", 2);
        creatureDictionary.Add("Fox", 5);
        creatureDictionary.Add("Wolf", 7);
        creatureDictionary.Add("Boar", 12);
        creatureDictionary.Add("Raccoon", 5);
        creatureDictionary.Add("Sparrow", 1);
        creatureDictionary.Add("Frog", 2);
        creatureDictionary.Add("Rabbit", 3);
        creatureDictionary.Add("Gecko", 1);
        creatureDictionary.Add("Horse", 15);

        void randomEncounter()
        {
            Console.Clear();
            Console.WriteLine("Line created via List:");
            var random = new Random();
            var random2 = new Random();
            int index = random.Next(creatureList.Count);
            int index2 = random.Next(locationList.Count);
            Console.WriteLine("Randomized: " + index);
            location = locationList[index2];
            creature = creatureList[index];
            Console.WriteLine($"You arrive at the {location}. An enemy {creature} appears!");
            Console.WriteLine();
        }

        void randomEncounterDict()
        {
            Console.WriteLine("Line created via Dictionary:");
            var random = new Random();
            int index = random.Next(creatureDictionary.Count);
            Console.WriteLine("Randomized: " + index);
            creature = creatureDictionary.ElementAt(index).Key;
            hitPoints = creatureDictionary.ElementAt(index).Value;
            Console.WriteLine($"An enemy {creature} appears! It has {hitPoints} hit points.");
            Console.WriteLine();
        }

    randomEncounter();
    randomEncounterDict();
