var name = "Rowan";
var profession = "Fighter";
var species = "Fox";

int expLevel = 1;
int expPoints = 0;

int hitPoints = 10;
int strength = 3;
int agility = 2;
int perception = 1;
int intelligence = 1;

Console.Clear();

RunProgram();

void RunProgram()
{
    RequestInfo();
    PrintInfo();
    Repeat();
}

void RequestInfo()
{
    Console.Clear();
    Console.WriteLine("Enter the character's name.");
    name = Console.ReadLine();
    Console.WriteLine();
    Console.WriteLine("Enter a species for " + name);
    species = Console.ReadLine();
    Console.WriteLine();
    Console.WriteLine("Enter a profession for " + name + " the " + species);
    profession = Console.ReadLine();
    Console.WriteLine();
}

void PrintInfo()
{
    Console.Clear();
    Console.WriteLine(name + " the " + species);
    Console.WriteLine("* * * * * * * * * *");
    Console.WriteLine();
    Console.WriteLine("Level " + expLevel.ToString() + " " + profession);
    Console.WriteLine(expPoints.ToString() + " XP");
    Console.WriteLine();
    Console.WriteLine("* * * * * * * * * *");
    Console.WriteLine(hitPoints.ToString() + " HP");
    Console.WriteLine("STR: " + strength.ToString() + "   AGI: " + agility.ToString());
    Console.WriteLine("PER: " + perception.ToString() + "   INT: " + intelligence.ToString());
    Console.WriteLine();
}

void Repeat()
{
    Console.WriteLine("Create another character? (Y/N)");
    ConsoleKeyInfo keyPress = Console.ReadKey();
    char hitKey = Char.ToUpper(keyPress.KeyChar);
    if (hitKey == 'Y') RunProgram();
}