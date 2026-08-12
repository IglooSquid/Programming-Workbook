int dieSize = 20;
int minimumSize = 2;
int maximumSize = 100;
int numberOfDice = 3;
int sum = 0;
List<int> resultList = new List<int>{};

char GetKeystroke()
{
    ConsoleKeyInfo keyPress = Console.ReadKey();
    char hitKey = Char.ToUpper(keyPress.KeyChar);
    return hitKey;
}

void AskPlayer()
{
    Console.Clear();
    Console.WriteLine($"Choose a D6 or a D20.");
    Console.WriteLine($"A - D6");
    Console.WriteLine($"B - D20");

    var key = GetKeystroke();

    switch (key)
    {
        case 'A':
            dieSize = 6;
            Console.WriteLine();
            Console.WriteLine("D6 selected.");
            break;
        case 'B':
            dieSize = 20;
            Console.WriteLine();
            Console.WriteLine("D20 selected.");
            break;
    }
}

void DieRoll()
{
    for (int i = 1; i <= numberOfDice; i++)
    {
        Console.WriteLine($"Rolling {numberOfDice} D{dieSize}...");        
        var random = new Random();
        int randomInt = random.Next(1, dieSize);
        Console.WriteLine($"Result {i}: {randomInt}");
        sum += randomInt;
        resultList.Add(randomInt);
    }

    Console.WriteLine($"Sum: {sum}");
    Console.WriteLine($"Max: {resultList.Max()}");
}

AskPlayer();
DieRoll();

