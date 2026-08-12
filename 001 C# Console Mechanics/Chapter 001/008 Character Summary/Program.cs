ConsoleKeyInfo keyPress;

// Character info
string characterName = "Rowan";
string characterRace = "Fox";
string characterClass = "Warrior";
// Character stats
int characterHealth = 0;
int characterStrength = 1;
int characterAgility = 1;
int characterPerception = 1;
int characterIntelligence = 1;

int currentLevel = 1;
int currentExperience = 0;

int startingHealthMin = 7;
int startingHealthMax = 12;

int minHealthIncrease = 0;
int maxHealthIncrease = 5;

int minStatIncrease = 0;
int maxStatIncrease = 2;

int experienceToLevelUp = 50;
float experienceMultiplier = 1.13f; // how much more XP each consequent level requires

Dictionary <char, string> classDictionary = new Dictionary <char, string>();
Dictionary <char, string> raceDictionary = new Dictionary <char, string>();
Dictionary <string, int> monsterDictionary = new Dictionary <string, int>();

List<string> adventureLog = new List<string>{};

string currentMonster;
int experienceReward;

int currentEncounter = 0;
int maxEncounters = 10;

InitializeProgram();
CharacterGeneration();
Encounter();

void InitializeProgram()
{
    characterHealth = 0;
    characterStrength = 1;
    characterAgility = 1;
    characterPerception = 1;
    characterIntelligence = 1;
    currentLevel = 1;
    currentExperience = 0;
    experienceToLevelUp = 100;

    classDictionary.Add('R', "Ranger");
    classDictionary.Add('S', "Shaman");
    classDictionary.Add('T', "Thief");
    classDictionary.Add('W', "Warrior");
    raceDictionary.Add('B', "Boar");
    raceDictionary.Add('F', "Fox");
    raceDictionary.Add('G', "Gecko");
    raceDictionary.Add('R', "Rabbit");
    raceDictionary.Add('W', "Wolf");
    monsterDictionary.Add("Rat", 10);
    monsterDictionary.Add("Bat", 11);
    monsterDictionary.Add("Skeleton", 18);
    monsterDictionary.Add("Goblin", 21);
    monsterDictionary.Add("Goblin Shaman", 27);
    monsterDictionary.Add("Goblin Chieftain", 31);
    monsterDictionary.Add("Orc Scout", 33);
    monsterDictionary.Add("Zombie", 34);
    monsterDictionary.Add("Orc Warrior", 39);
    monsterDictionary.Add("Orc Berserker", 45);
    monsterDictionary.Add("Orc Chieftain", 50);
    monsterDictionary.Add("Centaur", 52);
    monsterDictionary.Add("Centaur Hunter", 57);
    monsterDictionary.Add("Wyrm", 65);
    monsterDictionary.Add("Fire Elemental", 75);
    monsterDictionary.Add("Earth Elemental", 80);
    monsterDictionary.Add("Water Elemental", 82);
    monsterDictionary.Add("Air Elemental", 83);
    monsterDictionary.Add("Vampire", 88);
    monsterDictionary.Add("Drake", 92);
    monsterDictionary.Add("Lich", 100);
    Console.Clear();
    Console.WriteLine("================================");
    Console.WriteLine("PROGRAM START");
    Console.WriteLine("================================");
    Console.WriteLine();
}

void CharacterGeneration()
{
    Console.WriteLine("Enter a name for your character.");
    string playerEntry = Console.ReadLine();
    if (String.IsNullOrEmpty(playerEntry))
    {
        CharacterGeneration();
    }
    else
    {
        characterName = playerEntry;
        RaceSelection();
        ClassSelection();
        Console.WriteLine();
        var random = new Random();
        int startingHealth = random.Next(startingHealthMin, startingHealthMax);
        Console.WriteLine($"Rolling starting health: {startingHealth}");
        Console.WriteLine($"Adding racial and class bonuses to starting health: {characterHealth}");
        characterHealth += startingHealth;
        CharacterSummary();
    }
}

void CharacterSummary()
{
    Console.WriteLine();
    Console.WriteLine("#=- -=#=- -=#=- -=#=- -=#");
    Console.WriteLine($"{characterName}, the {characterRace} {characterClass}");
    Console.WriteLine("------------------------");
    Console.WriteLine($"Level {currentLevel} ({currentExperience}/{experienceToLevelUp} XP)");
    Console.WriteLine();
    Console.WriteLine($"{characterHealth} HP");
    Console.WriteLine($"STR: {characterStrength} | AGI: {characterAgility}");
    Console.WriteLine($"PER: {characterPerception} | INT: {characterIntelligence}");
    Console.WriteLine("#=- -=#=- -=#=- -=#=- -=#"); 
    Console.WriteLine();
}

void RaceSelection()
{
    Console.WriteLine();
    Console.WriteLine($"Select a Race for {characterName}.");
    Console.WriteLine("B - Boar");
    Console.WriteLine("F - Fox");
    Console.WriteLine("G - Gecko");
    Console.WriteLine("R - Rabbit");
    Console.WriteLine("W - Wolf");
    keyPress = Console.ReadKey();
    char selection = Char.ToUpper(keyPress.KeyChar);
    Console.WriteLine();

    switch (selection)
    {
        case 'B':
            characterRace = "Boar";
            characterStrength += 2;
            characterHealth += 3;
            break;
        case 'F':
            characterRace = "Fox";
            characterAgility += 1;
            characterPerception += 2;
            characterIntelligence += 1;
            break;
        case 'G':
            characterRace = "Gecko";
            characterAgility += 3;
            characterPerception += 1;
            break;
        case 'R':
            characterRace = "Rabbit";
            characterAgility += 2;
            characterPerception += 1;
            characterIntelligence += 1;
            break;
        case 'W':
            characterRace = "Wolf";
            characterStrength += 1;
            characterPerception += 2;
            characterHealth += 2;
            break;
        default:
            Console.WriteLine("Select one of the races by entering the associated initial letter.");
            RaceSelection();
            break;                
    }
}

void ClassSelection()
{
    Console.WriteLine();
    Console.WriteLine($"Select a Class for {characterName} the {characterRace}.");
    Console.WriteLine("R - Ranger");
    Console.WriteLine("S - Shaman");
    Console.WriteLine("T - Thief");
    Console.WriteLine("W - Warrior");
    keyPress = Console.ReadKey();
    char selection = Char.ToUpper(keyPress.KeyChar);
    Console.WriteLine(keyPress.KeyChar);
    Console.WriteLine();

    switch (selection)
    {
        case 'R':
            characterClass = "Ranger";
            characterPerception += 2;
            characterHealth += 1;
            break;
        case 'S':
            characterClass = "Shaman";
            characterIntelligence += 3;
            break;
        case 'T':
            characterClass = "Thief";
            characterAgility += 3;
            break;
        case 'W':
            characterClass = "Warrior";
            characterStrength +=2;
            characterHealth += 1;
            break;
        default:
            Console.WriteLine("Select one of the classes by entering the associated initial letter.");
            ClassSelection();
            break;    
    }
}

void Encounter()
{
    if (currentEncounter < maxEncounters)
    {
    Console.WriteLine();
    Console.WriteLine("* * * * * * * * * *");
    Console.WriteLine("NEW ENCOUNTER");
    Console.WriteLine("* * * * * * * * * *");
    currentEncounter++;
    Console.WriteLine("You see two paths, each with a waiting enemy. Choose your route.");
    var random = new Random();
    int index1 = random.Next(monsterDictionary.Count);
    int index2 = random.Next(monsterDictionary.Count);

    EncounterSelection(index1, index2);
    }

    else
        AdventureEnd();
}

void EncounterSelection(int index1, int index2)
{
    var monster1 = monsterDictionary.ElementAt(index1).Key;
    var monster2 = monsterDictionary.ElementAt(index2).Key;
    Console.WriteLine($"L - an enemy {monster1} along the left path.");
    Console.WriteLine($"R - an enemy {monster2} along the right path.");

    keyPress = Console.ReadKey();
    char selection = Char.ToUpper(keyPress.KeyChar);

    switch (selection)
    {
        case 'L':
            currentMonster = monster1;
            experienceReward = monsterDictionary.ElementAt(index1).Value;
            Console.WriteLine($"You head down the path on the left, towards the {currentMonster}.");
            Combat(currentMonster, experienceReward);
            break;
        case 'R':
            currentMonster = monster2;
            experienceReward = monsterDictionary.ElementAt(index2).Value;
            Console.WriteLine($"You head down the path on the right, towards the {currentMonster}.");
            Combat(currentMonster, experienceReward);
            break;
        default:
            Console.WriteLine("Select the left path (L) or the right path (R).");
            EncounterSelection(index1, index2);
            break;
    }    
}

void Combat(string monster, int experience)
{
    Console.WriteLine();
    Console.WriteLine("+ x + COMBAT ENCOUNTER + x +");
    Console.WriteLine();
    Console.WriteLine($"You engage the {monster} blocking your path in combat.");
    Console.WriteLine($"After an intense battle, you defeat the {monster}!");
    adventureLog.Add($"Defeated {monster}.");
    Console.WriteLine($"You gain {experience} XP.");
    ProcessExperience(experience);
}

void ProcessExperience(int experienceGain)
{
    currentExperience += experienceGain;
    Console.WriteLine($"Current experience: {currentExperience}/{experienceToLevelUp}");
    if (currentExperience < experienceToLevelUp)
        Encounter();
    else
        LevelUp();
}

void LevelUp()
{
        Console.WriteLine("* * * LEVEL UP * * *");
        Console.WriteLine("You have gained enough experience to level up!");
        currentExperience -= experienceToLevelUp;
        currentLevel++;
        Console.WriteLine($"You are now Level {currentLevel}.");
        adventureLog.Add($"Reached level {currentLevel}.");
        experienceToLevelUp = (int)Math.Round(experienceToLevelUp * experienceMultiplier, 0);
        Console.WriteLine("Stat increases:");
        StatIncrease();
        Console.WriteLine();
        Console.WriteLine("* * * * * * * * * *");
        if (currentExperience < experienceToLevelUp)
            Encounter();
        else
            LevelUp();
}

void StatIncrease()
{
    var randomHp = new Random();
    int healthIncrease = randomHp.Next(minHealthIncrease, maxHealthIncrease);
    characterHealth += healthIncrease;
    Console.WriteLine($"{characterHealth} (+{healthIncrease})");

    var randomStr = new Random();
    int strIncrease = randomStr.Next(minStatIncrease, maxStatIncrease);
    characterStrength += strIncrease;

    var randomAgi = new Random();
    int agiIncrease = randomAgi.Next(minStatIncrease, maxStatIncrease);
    characterAgility += agiIncrease;

    var randomPer = new Random();    
    int perIncrease = randomPer.Next(minStatIncrease, maxStatIncrease);
    characterPerception += perIncrease;

    var randomInt = new Random();
    int intIncrease = randomInt.Next(minStatIncrease, maxStatIncrease);
    characterIntelligence += intIncrease;

    Console.WriteLine($"STR: {characterStrength} (+{strIncrease}) | AGI: {characterAgility} (+{agiIncrease})");
    Console.WriteLine($"PER: {characterPerception} (+{perIncrease}) | INT: {characterIntelligence} (+{intIncrease})");
}

void AdventureEnd()
{
    Console.WriteLine();
    Console.WriteLine("* * * * * * * * * *");
    Console.WriteLine("You have reached the end of your adventure.");
    CharacterSummary();
    DisplayAdventureLog();
}

void DisplayAdventureLog()
{
    Console.WriteLine();
    Console.WriteLine("Here's a record of your journey:");
    foreach (var entry in adventureLog)
    {
        Console.WriteLine(entry);
    }
}