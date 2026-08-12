Dictionary<string, string> doorKey = new Dictionary<string, string>();

char[] letters =
{
    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K'
};

doorKey.Add("bronze key", "bronze lock");
doorKey.Add("silver key", "silver lock");
doorKey.Add("iron key", "iron lock");

bool allowForce = true;
string forceName = "Try to force the door open";

while (doorKey.Count > 0)
    Doors();

if (doorKey.Count == 0) Console.WriteLine($"You have opened all of the locks.");

void Doors()
{
    Console.Clear();
    Console.Write("You see before you ");
    for (int i = 1; i <= doorKey.Count; i++)
    {
        if (i == doorKey.Count && doorKey.Count > 1)
            Console.Write("and ");
        Console.Write($"a door locked with {doorKey.ElementAt(i-1).Value}");
        if (i < doorKey.Count)
            Console.Write(", ");
        else if (i == doorKey.Count)
            Console.Write(".");
    }

    Console.WriteLine();
    Console.WriteLine("Which lock would you like to open?");
    
    for (int j = 0; j < doorKey.Count; j++)
    {
        Console.WriteLine($"{letters[j]} - {doorKey.ElementAt(j).Value}");
    }

    Console.WriteLine();
    bool selectionRequired = true;

    while (selectionRequired)
    {
        ConsoleKeyInfo keyPress = Console.ReadKey(true);
        char key = Char.ToUpper(keyPress.KeyChar);

        int selectionKey = Array.IndexOf(letters, key);

        if (!letters.Contains(key)) continue;   // failsafe for non-included input

        if (selectionKey < doorKey.Count)
        {
            selectionRequired = false;
            string selection = doorKey.ElementAt(selectionKey).Value;
            Keys(selection);
        }
    }

}

void Keys(string targetLock)
{
    Console.Clear();
    Console.WriteLine($"What would you like to use to open the {targetLock}?");

    for (int index = 0; index < doorKey.Count; index++)
    {
        Console.WriteLine($"{letters[index]} - {doorKey.ElementAt(index).Key}");
    }

    int forceKeyIndex = doorKey.Count;
    char forceKey = letters[forceKeyIndex];
    if (allowForce)
        Console.WriteLine($"{forceKey} - {forceName}");

    bool selectionRequired = true;

    while (selectionRequired)
    {
        ConsoleKeyInfo keyPress = Console.ReadKey(true);
        char key = Char.ToUpper(keyPress.KeyChar);

        int selectionIndex = Array.IndexOf(letters, key);

        if (!letters.Contains(key)) continue;

        if (allowForce && selectionIndex == forceKeyIndex)
        {
            Console.Clear();
            selectionRequired = false;
            Console.WriteLine($"You attempt to force the {targetLock}, but it doesn't open.");
            Console.ReadKey(true);
        }

        if (selectionIndex <= doorKey.Count -1)
        {
            selectionRequired = false;
            if (doorKey.ElementAt(selectionIndex).Value == targetLock)
            {
                Console.Clear();
                Console.WriteLine($"The {doorKey.ElementAt(selectionIndex).Key} clicks into place, and the {targetLock} opens.");
                doorKey.Remove(doorKey.ElementAt(selectionIndex).Key);
                Console.ReadKey(true);
            }


            else
            {
                Console.Clear();
                Console.WriteLine($"The {doorKey.ElementAt(selectionIndex).Key} doesn't fit in the {targetLock}.");
                Console.ReadKey(true);
            }
        }
    }
}