bool isDebugActive = false;

Dictionary<string, float> monsterDictionary = new Dictionary<string, float>();
float currentExperience = 0f;
int currentLevel = 1;
int maxLevel = 5;
float experienceRequiredForLevel = 100f;
float experienceMultiplier = 1.12f;


Console.Clear();
InitializeMonsterDictionary();
GenerateEncounter();

void InitializeMonsterDictionary()
{
monsterDictionary.Add("Slime", 5f);      // 1
monsterDictionary.Add("Skeleton", 8f);   // 2
monsterDictionary.Add("Imp", 9f);        // 3
monsterDictionary.Add("Orc", 13f);       // 4
monsterDictionary.Add("Warlock", 17f);   // 5
monsterDictionary.Add("Wyrm", 21f);      // 6
}

int GetMonsterIndex()
{
    var random = new Random();
    int index = random.Next(monsterDictionary.Count);
    return index;
}

void GenerateEncounter()
{
    Console.WriteLine();
    int monsterIndex = GetMonsterIndex();
    string currentMonster = monsterDictionary.ElementAt(monsterIndex).Key;
    float experienceReward = monsterDictionary.ElementAt(monsterIndex).Value;
    Console.WriteLine($"You fight an enemy {currentMonster}!");
    Console.WriteLine($"You win! You gain {experienceReward} experience.");
    GainExperience(experienceReward);
}

void GainExperience(float gainedExperience)
{
    currentExperience += gainedExperience;
    float remainingExperience = experienceRequiredForLevel - currentExperience;
    if (isDebugActive) Console.WriteLine($"DEBUG: Current experience: {currentExperience}");
    if (currentExperience < experienceRequiredForLevel)
    {
        Console.WriteLine($"Experience needed to level up: {remainingExperience}");
        GenerateEncounter();
    }
    else
        LevelUp();
}

void LevelUp()
{
    currentExperience -= experienceRequiredForLevel;
    if (isDebugActive) Console.WriteLine($"DEBUG: Setting current experience to {currentExperience}...");
    experienceRequiredForLevel *= experienceMultiplier;
    if (isDebugActive) Console.WriteLine($"DEBUG: Experience required for next level is {experienceRequiredForLevel}.");
    currentLevel += 1;
    Console.WriteLine($"You have gained enough experience to level up to Level {currentLevel}!");
    if (currentLevel >= maxLevel)
        Console.WriteLine($"Reached maximum level {maxLevel}. Terminating simulation.");
    else
        GenerateEncounter();
}