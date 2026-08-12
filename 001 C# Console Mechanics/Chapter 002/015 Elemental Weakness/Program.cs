int numberOfRounds = 3;
int currentRound = 0;

Element weakness;
Element resistance;
Element attackType;

Element Fire = new Element("Fire");
Element Water = new Element("Water");
Element Wood = new Element("Wood");
Element Gold = new Element("Gold");
Element Earth = new Element("Earth");

Element[] elements = {Fire, Water, Wood, Gold, Earth};
char[] letters = {'A', 'B', 'C', 'D', 'E', 'F', 'G'};

string weaknessMessage = "It's super effective!";
string resistanceMessage = "The effect is resisted.";
string attackMessage = "Attack initiated.";

Simulation();

void InterfaceUpdate()
{
    Console.Clear();
    Console.WriteLine($"Round {currentRound} of {numberOfRounds}");
    Console.WriteLine();
    Console.Write("Weakness: ");
    if (weakness != null) Console.Write(weakness.name);
    else Console.Write("");
    Console.WriteLine();
    Console.Write("Resistance: ");
    if (resistance != null) Console.Write(resistance.name);
    else Console.Write("");
    Console.WriteLine();
    Console.Write("Attack type: ");
    if (attackType != null) Console.Write(attackType.name);
    else Console.Write("");
    Console.WriteLine();
    Console.WriteLine("------------------------");
    Console.WriteLine();
}

void Simulation()
{
    for (currentRound = 1; currentRound <= numberOfRounds; currentRound++)
    {
        weakness = null;
        resistance = null;
        attackType = null;

        weakness = elements[WeaknessSetup()];
        resistance = elements[ResistanceSetup()];
        attackType = elements[AttackSetup()];
        AttackSimulation();
    }
}

int WeaknessSetup()
{
    bool selectionRequired = true;
    int value = 0;    
    while (selectionRequired)
    {    
        InterfaceUpdate();
        Console.WriteLine("Select active elemental weakness.");
        Console.WriteLine();
        foreach (var entry in elements)
        {
            int index = Array.IndexOf(elements, entry);
            Console.WriteLine($"{letters[index]} - {entry.name}");
        }

        ConsoleKeyInfo keyPress = Console.ReadKey(true);
        char hitKey = Char.ToUpper(keyPress.KeyChar);

        if (!letters.Contains(hitKey)) continue;

        int selectionKey = Array.IndexOf(letters, hitKey);

        if (selectionKey < elements.Length)
        {
            selectionRequired = false;
            InterfaceUpdate();
            Console.WriteLine($"{elements[selectionKey].name} selected.");
            Console.WriteLine($"Press any key to continue.");
            Console.ReadKey(true);
            value = selectionKey;
        }
    }
    return value;
}

int ResistanceSetup()
{
    bool selectionRequired = true;
    int value = 0;
    while (selectionRequired)
    {    
        InterfaceUpdate();
        Console.WriteLine("Select active elemental resistance.");
        Console.WriteLine();
        foreach (var entry in elements)
        {
            int index = Array.IndexOf(elements, entry);
            Console.WriteLine($"{letters[index]} - {entry.name}");
        }


        ConsoleKeyInfo keyPress = Console.ReadKey(true);
        char hitKey = Char.ToUpper(keyPress.KeyChar);

        if (!letters.Contains(hitKey)) continue;

        int selectionKey = Array.IndexOf(letters, hitKey);

        if (elements[selectionKey] == weakness)
        {
            InterfaceUpdate();
            Console.WriteLine($"Cannot use the same element {weakness.name} for both weakness and resistance.");
            Console.WriteLine($"Press any key to continue.");
            Console.ReadKey(true);
        }

        else if (selectionKey < elements.Length)
        {
            selectionRequired = false;
            InterfaceUpdate();
            Console.WriteLine($"{elements[selectionKey].name} selected.");
            Console.WriteLine($"Press any key to continue.");
            Console.ReadKey(true);
            value = selectionKey;       
        }
    }
    return value;
}

int AttackSetup()
{
    bool selectionRequired = true;
    int value = 0;    
    while (selectionRequired)
    {
        InterfaceUpdate();
        Console.WriteLine("Select attack type.");
        Console.WriteLine();
        foreach (var entry in elements)
        {
            int index = Array.IndexOf(elements, entry);
            Console.WriteLine($"{letters[index]} - {entry.name}");
        }

        ConsoleKeyInfo keyPress = Console.ReadKey(true);
        char hitKey = Char.ToUpper(keyPress.KeyChar);

        if (!letters.Contains(hitKey)) continue;

        int selectionKey = Array.IndexOf(letters, hitKey);

        if (selectionKey < elements.Length)
        {
            selectionRequired = false;
            InterfaceUpdate();
            Console.WriteLine($"{elements[selectionKey].name} selected.");
            Console.WriteLine($"Press any key to continue.");
            Console.ReadKey(true);
            value = selectionKey;    
        }
    }
    return value;
}

void AttackSimulation()
{
    InterfaceUpdate();
    Console.WriteLine($"COMBAT INITIATED");
    Console.WriteLine("* * * * * * * * * *");
    Console.WriteLine();
    Console.WriteLine(attackMessage);
    Console.WriteLine($"Attack type: {attackType.name}.");
    if (attackType == weakness)
        Console.WriteLine(weaknessMessage);
    if (attackType == resistance)
        Console.WriteLine(resistanceMessage);
    Console.WriteLine();
    Console.WriteLine($"Press any key to continue.");
    Console.ReadKey(true);               
}