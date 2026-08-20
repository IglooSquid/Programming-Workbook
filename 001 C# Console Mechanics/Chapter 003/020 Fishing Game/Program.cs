using FishingGame;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FishingGame
{
    public class Program
    {
        public static Weather.WeatherState currentWeather;
        public static int currentTime = 0;
        public static int timeLimit = 60;
        public static byte weatherChangeRate = 40;

        public static byte fishChance = 45;
        public static byte treasureChance = 10;
        public static byte trashChance = 25;

        public static byte totalChance = 100;

        public static byte rainBonus = 15;

        public static int caughtFish = 0;
        public static int caughtFishValue = 0;
        public static int foundTreasure = 0;
        public static int foundTreasureValue = 0;

        public static List<Item> fishList = new List<Item>();
        public static List<Item> treasureList = new List<Item>();
        public static List<Item> trashList = new List<Item>();
        public static byte waitTime = 250;

        public static List<Item> fishCatchList = new List<Item>();
        public static List<Item> treasureCatchList = new List<Item>();
        public static List<Item> trashCatchList = new List<Item>();


        public static void Main(String[] args)
        {
            Initialize();
            PopulateLists();
            Refresh();

            while (currentTime < timeLimit)
            {
                Fish();
                ProcessWeather();
            }
        }

        public static void Wait(int duration)
        {
            System.Threading.Thread.Sleep(duration);
        }

        public static void Fish()
        {
            Refresh();
            Console.Beep();
            Console.Write("Fishing");
            currentTime++;
            Wait(waitTime);
            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                currentTime++;
                Wait(waitTime);
            }
            Console.WriteLine();
            RollCatch();
        }

        public static void RollCatch()
        {
            if (currentWeather == Weather.WeatherState.Rainy)
            {
                fishChance += rainBonus;
            }

            int catchChance = fishChance + treasureChance + trashChance;

            var random = new Random();
            int roll = random.Next(totalChance);

            if (roll <= fishChance)
            {
                Catch(Item.ItemCategory.Fish);
            }
            else if (roll > fishChance && roll <= fishChance + treasureChance)
            {
                Catch(Item.ItemCategory.Treasure);
            }
            else if (roll > fishChance + treasureChance && roll <= catchChance)
            {
                Catch(Item.ItemCategory.Trash);
            }
            else
            {
                Console.WriteLine($"Nothing bites.");
                Console.ReadKey(true);
            }
        }

        public static void Catch(Item.ItemCategory category)
        {
            Console.WriteLine("You've caught something!");
            Console.ReadKey(true);
            Refresh();

            switch (category)
            {
                case Item.ItemCategory.Fish:
                    fishCatchList.Clear();
                    foreach (var entry in fishList)
                    {
                        for (int i = 0; i < entry.BaseCatchChance; i++)
                        {
                            fishCatchList.Add(entry);
                        }
                    }

                    var random = new Random();
                    int index = random.Next(fishCatchList.Count);
                    Item Catch = fishCatchList[index];
                    caughtFish++;
                    caughtFishValue += Catch.Value;
                    Refresh();
                    Console.WriteLine($"Successfully caught {Catch.Name}.");
                    break;

                case Item.ItemCategory.Treasure:
                    treasureCatchList.Clear();
                    foreach (var entry in treasureList)
                    {
                        for (int i = 0; i < entry.BaseCatchChance; i++)
                        {
                            treasureCatchList.Add(entry);
                        }
                    }

                    var random2 = new Random();
                    int index2 = random2.Next(treasureCatchList.Count);
                    Item Catch2 = treasureCatchList[index2];
                    foundTreasure++;
                    foundTreasureValue += Catch2.Value;
                    Refresh();
                    Console.WriteLine($"You've found {Catch2.Name}.");
                    break;

                case Item.ItemCategory.Trash:
                    trashCatchList.Clear();
                    foreach (var entry in trashList)
                    {
                        for (int i = 0; i < entry.BaseCatchChance; i++)
                        {
                            trashCatchList.Add(entry);
                        }
                    }

                    var random3 = new Random();
                    int index3 = random3.Next(trashCatchList.Count);
                    Item Catch3 = trashCatchList[index3];
                    Refresh();
                    Console.WriteLine($"It's just {Catch3.Name}.");
                    break;
            }

            Console.ReadKey(true);        
        }

        public static void Refresh()
        {
            try
            {
                Console.Clear();
                Console.WriteLine($"Weather: {currentWeather}");
                Console.WriteLine($"Time: {currentTime}");
                Console.WriteLine($"Fish caught: {caughtFish} (total value {caughtFishValue})");
                Console.WriteLine($"Treasure found: {foundTreasure} (total value {foundTreasureValue})");
                Console.WriteLine("==============================================");
                Console.WriteLine();
            }
            catch
            {
                Console.Clear();
                Console.WriteLine($"Unable to refresh UI.");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public static void PopulateLists()
        {
            Item Trout = new Item("trout", 100, 50, Item.ItemCategory.Fish);
            Item Bass = new Item("bass", 75, 60, Item.ItemCategory.Fish);
            Item Salmon = new Item("salmon", 180, 25, Item.ItemCategory.Fish);

            Item Fusaka = new Item("fusaka", 1000, 10, Item.ItemCategory.Treasure);
            Item Ring = new Item("ring", 250, 20, Item.ItemCategory.Treasure);
            Item Chest = new Item("chest", 500, 5, Item.ItemCategory.Treasure);

            Item Boot = new Item("boot", 5, 20, Item.ItemCategory.Trash);
            Item Tire = new Item("tire", 10, 10, Item.ItemCategory.Trash);
            Item Seaweed = new Item("seaween", 1, 25, Item.ItemCategory.Trash);

            fishList.Add(Trout);
            fishList.Add(Bass);
            fishList.Add(Salmon);

            treasureList.Add(Fusaka);
            treasureList.Add(Ring);
            treasureList.Add(Chest);

            trashList.Add(Boot);
            trashList.Add(Tire);
            trashList.Add(Seaweed);
        }

        public static void Initialize()
        {
            currentWeather = Weather.GenerateWeather();
        }

        public static void ProcessWeather()
        {
            Weather.WeatherState generatedWeather;
            var random = new Random();
            int roll = random.Next(1, 101);
            if (roll < weatherChangeRate)
            {
                do
                {
                    generatedWeather = Weather.GenerateWeather();
                }
                while (generatedWeather == currentWeather);

                currentWeather = generatedWeather;

                Refresh();
                Console.WriteLine($"The weather is now {currentWeather}.");
            }
            else
            {
                Refresh();
                Console.WriteLine($"The weather is still {currentWeather}.");
            }

            Console.ReadKey(true);
        }
    }
}