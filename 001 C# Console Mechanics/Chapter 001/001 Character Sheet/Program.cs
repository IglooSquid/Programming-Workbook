
string name = "Clyde";
string species = "Fox";
string profession = "Fighter";
int level = 1;
int experience = 23;

int hitPoints = 20;
int statStrength = 4;
int statAgility = 2;
int statIntelligence = 2;
int statPerception = 3;

List<string> nameList = new List<string>{"Clyde", "Rufus", "Fritz", "Felix"};
List<string> speciesList = new List<string>{"Wolf", "Fox", "Raccoon", "Boar"};
List<string> classList = new List<string>{"Warrior", "Ranger", "Knight", "Thief"};

int hitPointsMin = 10;
int hitPointsMax = 25;
int statMin = 1;
int statMax = 8;

int sheetsToPrint = 2;

Console.Clear();
PrintInfo();
// GenerateCharacter();


void GenerateCharacter()
{
    for (int i = 0; i < sheetsToPrint; i++)
    {
        var random1 = new Random();   
        int index = random1.Next(nameList.Count);
        name = nameList[index];

        var random2 = new Random();   
        int index = random2.Next(speciesList.Count);
        species = speciesList[index];

        var random3 = new Random();   
        int index = random3.Next(classList.Count);
        class = classList[index];        

        var random4 = new Random();
        hitPoints = random4.Next(hitPointsMin, hitPointsMax);

        var random5 = new Random();
        statStrength = random5.Next(statMin, statMax);

        var random6 = new Random();
        statAgility = random6.Next(statMin, statMax);

        var random7 = new Random();
        statIntelligence = random7.Next(statMin, statMax);

        var random8 = new Random();
        statPerception = random8.Next(statMin, statMax);

        PrintInfo();
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
    }
}


void PrintInfo()
{
    Console.WriteLine(name + " the " + species + " " + profession);
    Console.WriteLine("====================");  
    Console.WriteLine("Level: " + level + " (" + experience + "XP)");
    Console.WriteLine();
    Console.WriteLine("HP: " + hitPoints);
    Console.WriteLine("Strength: " + statStrength);
    Console.WriteLine("Agility: " + statAgility);
    Console.WriteLine("Intelligence: " + statIntelligence);
    Console.WriteLine("Perception: " + statPerception);
    Console.WriteLine();
}