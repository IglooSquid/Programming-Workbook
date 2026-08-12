int currentLevel = 1;
int currentExperience = 0;
int maxLevel = 10;
int experienceToLevelUp = 50;
// float remainingExperience = experienceToLevelUp;
float levelMultiplier = 1.32f;

int characterHitPoints = 10;
int characterHitPointsMinIncrease = 1;
int characterHitPointsMaxIncrease = 7;

Dictionary<string, int> enemyDictionary = new Dictionary<string, int>();

Console.Clear();
InitializeEnemyDictionary();

Console.WriteLine("===========================================");
Console.WriteLine("Initializing program.");
Console.WriteLine("===========================================");
Console.WriteLine();
GenerateEncounter(); // program starts here

void InitializeEnemyDictionary()
{
    enemyDictionary.Add("Goblin", 10);
    enemyDictionary.Add("Goblin Soldier", 15);
    enemyDictionary.Add("Goblin Shaman", 19);
    enemyDictionary.Add("Goblin Chief", 28);
    enemyDictionary.Add("Orc", 16);
    enemyDictionary.Add("Orc Warrior", 27);
    enemyDictionary.Add("Orc Chieftain", 40);
    enemyDictionary.Add("Zombie", 13);
    enemyDictionary.Add("Ghoul", 23);
    enemyDictionary.Add("Revenant", 33);
    enemyDictionary.Add("Lich", 43);
    enemyDictionary.Add("Wurm", 25);
    enemyDictionary.Add("Drake", 40);
    enemyDictionary.Add("Wyvern", 60);
    enemyDictionary.Add("Kuo-toa", 25);
    enemyDictionary.Add("Sahuagin", 50);
    enemyDictionary.Add("Merfolk", 70);
    enemyDictionary.Add("Dragon", 250);
}

void LevelUp()
{
    Console.WriteLine($"You have gained enough experience to level up!");
    currentExperience -= experienceToLevelUp;
    currentLevel++;
    experienceToLevelUp = (int)Math.Round(experienceToLevelUp * levelMultiplier, 0);
    Console.WriteLine($"You are now level {currentLevel}.");
    Console.WriteLine($"Current experience: {currentExperience}/{experienceToLevelUp}");
    HealthGain();
    if (currentLevel >= maxLevel)
    {
        Console.WriteLine($"Reached maximum level of {maxLevel}.");
        Console.WriteLine("Terminating program.");
        Console.WriteLine("* * * * * * * * * *");
    }
    else if (currentExperience >= experienceToLevelUp)
    {
        Console.WriteLine($"You feel you haven't finished powering up!");
        LevelUp();
    }
    else
        GenerateEncounter();
}

void HealthGain()
{
    Console.WriteLine($"You feel stronger!");
    Random random = new Random();
    int healthGain = random.Next(characterHitPointsMinIncrease, characterHitPointsMaxIncrease);
    Console.WriteLine($"You gain {healthGain} hit points.");
    characterHitPoints += healthGain;
    Console.WriteLine($"You now have {characterHitPoints} hit points.");
}

void ExperienceGain(int experienceGain)
{
    Console.WriteLine($"You gain {experienceGain} experience.");
    currentExperience += experienceGain;
    Console.WriteLine($"Current experience: {currentExperience}/{experienceToLevelUp}");
    if (currentExperience < experienceToLevelUp)
        GenerateEncounter();
    else
        LevelUp();
}

void GenerateEncounter()
{
    Console.WriteLine();
    Console.WriteLine("* * * * * * * * * *");
    Console.WriteLine("NEW ROUND");
    Console.WriteLine("* * * * * * * * * *");    
    Console.WriteLine();
    var random = new Random();
    int index = random.Next(enemyDictionary.Count);
    string currentEnemy = enemyDictionary.ElementAt(index).Key;
    int experienceReward = enemyDictionary.ElementAt(index).Value;
    Console.WriteLine($"An enemy {currentEnemy} appears!");
    Console.WriteLine($"You win!");
    ExperienceGain(experienceReward);
}