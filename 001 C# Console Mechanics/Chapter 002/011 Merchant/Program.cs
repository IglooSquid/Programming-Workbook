using System;
using System.Collections.Generic;

int playerGold = 1000;
bool isLeaving = false;

Dictionary<string, int> merchantInventory = new Dictionary<string, int>();

merchantInventory.Add("Food ration", 200);
merchantInventory.Add("Broadsword", 500);
merchantInventory.Add("Spellbook", 800);
merchantInventory.Add("Plate mail", 1200);

List<char> inventoryKey = new List<char>
{
    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'
};

do
{
MerchantDialogue();
} while (!isLeaving);

void UpdateGold()
{
    Console.Clear();
    Console.WriteLine($"Your gold: {playerGold}");
}

void MerchantDialogue()
{
    UpdateGold();
    Console.WriteLine($"Welcome to the shop. What would you like to purchase?");
    Console.WriteLine();
    for (int i = 0; i < merchantInventory.Count; i++)
    {
        Console.WriteLine($"{inventoryKey[i]} - {merchantInventory.ElementAt(i).Key}, costs {merchantInventory.ElementAt(i).Value} gold");
    }
    Console.WriteLine();
    Console.WriteLine($"X - Leave shop");

    ConsoleKeyInfo keyPress = Console.ReadKey(true);
    char hitKey = Char.ToUpper(keyPress.KeyChar);

    if (hitKey == 'X')
    {
        UpdateGold();
        isLeaving = true;
        Console.WriteLine($"Be seeing you!");
        Console.ReadKey(true);
    }

    else if (inventoryKey.Contains(hitKey))
    {
        int selectedIndex = inventoryKey.IndexOf(hitKey);

        if (playerGold < (int)merchantInventory.ElementAt(selectedIndex).Value)
        {
            UpdateGold();
            Console.WriteLine($"You don't have enough gold for the {merchantInventory.ElementAt(selectedIndex).Key}.");
            Console.WriteLine($"Press any key to continue.");
            Console.ReadKey(true);
        }
        else
        {
            UpdateGold();
            Console.WriteLine($"Do you want to buy the {merchantInventory.ElementAt(selectedIndex).Key} for {merchantInventory.ElementAt(selectedIndex).Value} gold? Y/N");

            ConsoleKeyInfo keyPress2 = Console.ReadKey(true);
            char hitKey2 = Char.ToUpper(keyPress2.KeyChar);

            switch (hitKey2)
            {
                case 'N':
                    break;
                case 'Y':
                    playerGold -= merchantInventory.ElementAt(selectedIndex).Value;
                    UpdateGold();
                    Console.WriteLine($"Purchased {merchantInventory.ElementAt(selectedIndex).Key} for {merchantInventory.ElementAt(selectedIndex).Value} gold.");
                    merchantInventory.Remove(merchantInventory.ElementAt(selectedIndex).Key);
                    Console.WriteLine($"Press any key to continue.");
                    Console.ReadKey(true);
                    break;
            }
        }
    }
}