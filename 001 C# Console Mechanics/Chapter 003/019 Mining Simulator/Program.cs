using System;
using System.Collections.Generic;
using MiningSimulator;

public class Program
{
    Item Rock = new Item("Rock", 30);
    Item Copper = new Item("Copper Ore", 20);
    Item Iron = new Item("Iron Ore", 20);
    Item Onyx = new Item("Onyx", 5);
    Item Topaz = new Item("Topaz", 4);
    Item Ruby = new Item("Ruby", 3);
    Item Emerald = new Item("Emerald", 2);
    Item Diamond = new Item("Diamond", 1);

    public List<Item> itemList = new List<Item>();
    public List<Item> poorList = new List<Item>();
    public List<Item> mediumList = new List<Item>();
    public List<Item> rareList = new List<Item>();
    public List <Node> nodeList = new List<Node>();

    Node Poor = new Node("Poor node", 3);
    Node Medium = new Node("Medium node", 5);
    Node Rare = new Node("Rare node", 7);

    Node currentNode = null;
    int currentNodeCharges = 0;
    List<Item> currentNodeList;

    public bool isMining = false;

    public Dictionary<Node, List<Item>> nodeDictionary = new Dictionary<Node, List<Item>>();

    int playerStamina = 25;
    int playerStaminaCurrent = 0;
    int miningSuccessRate = 90;
    public List<string> lootList = new List<string>();
    int itemsFound = 0;
    
    public static void Main(String[] args)
    {
        Program program = new Program();
        program.playerStaminaCurrent = program.playerStamina;
        program.PopulateLists();
        program.Refresh();
        program.GenerateNode();
        program.isMining = true;

        while (program.isMining)
        {
            program.MiningAction();
        }

        program.Refresh();

        Console.WriteLine($"Items found:");
        foreach (var entry in program.lootList)
        {
            Console.WriteLine(entry);
        }
        Console.ReadKey(true);
    }

    public void Wait(int duration)
    {
        System.Threading.Thread.Sleep(duration);
    }

    public void MiningAction()
    {
        Refresh();

        if (playerStaminaCurrent <= 0)
        {
            Console.WriteLine($"Stamina exhausted.");
            Console.ReadKey(true);
            isMining = false;
        }

        else if (currentNodeCharges <= 0)
        {
            Console.WriteLine($"Current node exhausted. Generating new node.");
            Console.ReadKey(true);
            GenerateNode();
        }

        else
        {
            Console.WriteLine($"Press F to mine.");

            bool inputRequired = true;

            while (inputRequired)
            {
                ConsoleKeyInfo keyPress = Console.ReadKey(true);
                char key = Char.ToUpper(keyPress.KeyChar);
                if (key == 'F')
                {
                    inputRequired = false;
                    Mine();
                }
            }

        }
    }

    public void Mine()
    {
        playerStaminaCurrent--;
        Refresh();
        MiningSounds();

        var random = new Random();
        int roll = random.Next(0, 100);
        if (roll > miningSuccessRate)
        {
            Refresh();
            Console.WriteLine($"You find nothing.");
            Console.ReadKey(true);
        }
        else
        {
            currentNodeCharges--;
            var random2 = new Random();
            int roll2 = random.Next(currentNodeList.Count);
            Item foundItem = currentNodeList[roll2];
            lootList.Add(foundItem.Name);
            itemsFound++;
            Refresh();
            Console.WriteLine($"Found {foundItem.Name}.");
            Console.ReadKey(true);
        }

    }

    public void MiningSounds()
    {
        var random1 = new Random();


        int delay1 = random1.Next(400, 800);


        Console.Beep();
        Wait(delay1);
        Console.Beep();
        Wait(100);
    }

    public void Refresh()
    {
        try
        {
        if (currentNode.Name == null) return;
        string nodeName = currentNode.Name;
        if (nodeName == null) nodeName = "None";
        Console.Clear();
        Console.WriteLine($"Current stamina: {playerStaminaCurrent}");
        Console.WriteLine($"Items found: {itemsFound}");
        Console.WriteLine($"Current node: {currentNode.Name}");
        }
        catch
        {
            Console.WriteLine($"Exception detected, skipping UI refresh");
        }
    }

    public void GenerateNode()
    {
        Refresh();
        var random = new Random();
        int index = random.Next(nodeDictionary.Count);

        currentNode = nodeDictionary.ElementAt(index).Key;
        currentNodeCharges = currentNode.Charges;
        currentNodeList = nodeDictionary.ElementAt(index).Value;

        Console.WriteLine($"New node: {currentNode.Name}");
        Console.WriteLine($"Node charges: {currentNodeCharges}");
        Console.WriteLine();
        isMining = true;        
        Console.ReadKey(true);
    }

    public void PopulateLists()
    {
        itemList.Add(Rock);
        itemList.Add(Copper);
        itemList.Add(Iron);
        itemList.Add(Onyx);
        itemList.Add(Topaz);
        itemList.Add(Ruby);
        itemList.Add(Emerald);
        itemList.Add(Diamond);

        nodeList.Add(Poor);
        nodeList.Add(Medium);
        nodeList.Add(Rare);

        nodeDictionary.Add(Poor, poorList);
        nodeDictionary.Add(Medium, mediumList);
        nodeDictionary.Add(Rare, rareList);

        foreach (var item in itemList)
        {
            if (item.Rarity >= 10)
            {
                poorList.Add(item);
                Console.WriteLine($"Added {item.Name} to low-rarity list.");
                mediumList.Add(item);
                Console.WriteLine($"Added {item.Name} to medium-rarity list.");
            }

            if (item.Rarity >= 3 && item.Rarity < 10)
            {
                mediumList.Add(item);
                Console.WriteLine($"Added {item.Name} to medium-rarity list.");
                rareList.Add(item);
                Console.WriteLine($"Added {item.Name} to high-rarity list.");
            }

            if (item.Rarity < 3)
            {
                rareList.Add(item);
                Console.WriteLine($"Added {item.Name} to high-rarity list.");
            }
        }

        Console.WriteLine($"Low rarity list contents:");
        foreach (var lowRarityItem in poorList)
        {
            Console.WriteLine(lowRarityItem.Name);
        }        
        Console.WriteLine();

        Console.WriteLine($"Medium rarity list contents:");
        foreach (var mediumRarityItem in mediumList)
        {
            Console.WriteLine(mediumRarityItem.Name);
        }        
        Console.WriteLine();

        Console.WriteLine($"High rarity list contents:");
        foreach (var highRarityItem in rareList)
        {
            Console.WriteLine(highRarityItem.Name);
        }        
        Console.WriteLine();
        Console.ReadKey(true);
    }
}