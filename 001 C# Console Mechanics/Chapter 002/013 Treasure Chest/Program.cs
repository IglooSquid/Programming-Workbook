Item Sword = new Item("a sword", 10, 25);
Item Torch = new Item("a torch", 8, 5);
Item Potion = new Item("a potion", 3, 15);
Item Jewel = new Item("a jewel", 5, 120);
Item Spellbook = new Item("a spellbook", 6, 75);
Item Helmet = new Item("a helmet", 10, 50);

List<Item> lootListBasic = new List<Item>{Sword, Torch, Spellbook};
List<Item> lootListModerate = new List<Item>{Sword, Potion, Spellbook, Helmet};
List<Item> lootListFine = new List<Item>{Spellbook, Jewel, Helmet};

Chest WoodenChest = new Chest("Wooden Chest", lootListBasic);
Chest IronChest = new Chest("Iron Chest", lootListModerate);
Chest GoldenChest = new Chest("Golden Chest", lootListFine);

List<Chest> chestList = new List<Chest>{WoodenChest, IronChest, GoldenChest};

bool playerIsExiting = false;

char[] letters = {'A', 'B', 'C', 'D', 'E', 'F'};

string exitText = "Exit";

while (!playerIsExiting)
    Chests();

void Chests()
{
    Console.Clear();
    if (chestList.Count > 1)
    {
        Console.WriteLine($"You see {chestList.Count} chests. Which do you wish to open?");
    }
    else if (chestList.Count == 1)
    {
        Console.WriteLine($"You see one {chestList[0].name}.");
    }

    Console.WriteLine();

    for (int i = 0; i < chestList.Count; i++)
    {
        Console.WriteLine($"{letters[i]} - {chestList[i].name}");
    }

    int exitKeyIndex = chestList.Count;
    char exitKey = Char.ToUpper(letters[exitKeyIndex]);

    Console.WriteLine($"{exitKey} - {exitText}");

    Console.WriteLine();

    bool selectionRequired = true;

    while (selectionRequired)
    {
        ConsoleKeyInfo keyPress = Console.ReadKey(true);
        char key = Char.ToUpper(keyPress.KeyChar);

        int selectionKey = Array.IndexOf(letters, key);

        if (!letters.Contains(key)) continue;

        if (selectionKey == exitKeyIndex)
        {
            Console.Clear();
            Console.WriteLine($"Be seeing you.");
            playerIsExiting = true;
            Console.ReadKey(true);
            break;
        }

        if (selectionKey < chestList.Count)
        {
            Console.Clear();
            selectionRequired = false;
            Chest selectedChest = chestList[selectionKey];

            if (selectedChest.isOpened == true)
            {
                Console.WriteLine($"You have already opened the {selectedChest.name}. It's empty.");
                Console.ReadKey(true);
            }

            else
            {
                selectedChest.isOpened = true;
                Console.WriteLine($"You carefully open the {selectedChest.name}...");

                var random = new Random();
                int index = random.Next(selectedChest.contentsList.Count);
                Item reward = selectedChest.contentsList[index];

                Console.WriteLine($"You find {reward.name} in the {selectedChest.name}.");
                Console.ReadKey(true);
            }
        }
    }
}
