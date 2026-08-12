int dialoguePath;
int dialogueBranch = 0;
string defaultFarewell = "See you later then.";

int Dialogue(string dialogueLine, params string[] dialogueOptions)
{
    Console.WriteLine($"'{dialogueLine}'");
    Console.WriteLine();

    int index = 0;

    foreach (string entry in dialogueOptions)
    {
        Console.WriteLine($"{index+1} - {entry}");
        index++;
    }

    bool responseRequired = true;

    while (responseRequired)
    {
        int number;
        ConsoleKeyInfo keyPress = Console.ReadKey(true);
        if (char.IsDigit(keyPress.KeyChar))
        {
            number = int.Parse(keyPress.KeyChar.ToString());
            int selection = number - 1;

            if (selection < dialogueOptions.Length && selection >= 0)
            {
                responseRequired = false;
                Console.Clear();
                Console.WriteLine($"You say: '{dialogueOptions[selection]}'.");
                Console.WriteLine();
                return selection;      
            }
        }
    }

    return dialogueOptions.Length+1;
}

dialoguePath = Dialogue(
    "Welcome. How can I help you?", // lead
    "Show me your wares.", // 0
    "Any rumors?", // 1
    "I'm looking for work.", // 2
    "Goodbye." // 3
);

switch (dialoguePath)
{
    case 0:
        Console.WriteLine($"Alright, let's do business.");
        break;
    case 1:
        dialoguePath = Dialogue(
            "Just the usual. Rats in basements, orcs in the borderlands, and crooked nobles ruining everything for everybody.", // lead
            "What's this about orcs?", // 0
            "Show me your wares.", // 1
            "Goodbye." // 2
        );
        dialogueBranch = 0;
        break;
    case 2: 
        dialoguePath = Dialogue(
            "Go ask old man Tevin at the tavern. He usually hears a lot of leads for mercs.", // lead
            "Thank you, goodbye." // 0
        );
        dialogueBranch = 1;
        break;
    case 3:
        Console.WriteLine(defaultFarewell);
        break;
    default:
        Console.WriteLine(defaultFarewell);
        break;
}

switch (dialogueBranch)
{
    case 0:
        switch (dialoguePath)
        {
            case 0:
                Console.WriteLine($"The orcs? That's been going on for a while. If you're looking to partake, Marshall Grey at the Westwatch Bastion will probably be happy for any volunteers he gets.");
                break;
            case 1:
                Console.WriteLine($"Alright, let's do business.");
                break;
            default:
                Console.WriteLine(defaultFarewell);
                break;
        }
        break;
        
    case 1:
    switch (dialoguePath)
        {
            case 0:
                Console.WriteLine($"No worries. Take care now.");
                break;
            default:
                Console.WriteLine(defaultFarewell);
                break; 
        }
        break;
}