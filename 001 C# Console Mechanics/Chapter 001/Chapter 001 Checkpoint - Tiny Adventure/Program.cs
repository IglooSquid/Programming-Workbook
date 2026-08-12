public class Program
{
    public static void Main ()
    {
        string playerName = null;
        PlayerRace playerRace = null;
        PlayerClass playerClass = null;

        int playerLevel = 0;
        int playerExperience = 0;
        int playerHealthMax = 0;
        int playerHealthCurrent = 0;
        int playerStrength = 0;
        int playerAgility = 0;

        int playerBaseHealth = 10;
        int playerBaseStrength = 1;
        int playerBaseAgility = 1;

        int healthIncreaseMin = 1;
        int healthIncreaseMax = 3;
        int statIncreaseMin = 0;
        int statIncreaseMax = 2;

        int experienceToLevelUp = 100;
        float experienceMultiplier = 1.15f;

        int maxInstances = 10;
        int currentInstance = 0;

        bool playerIsAlive = true;
        bool monsterIsAlive = false;

        // Race initialization
        PlayerRace Boar = new PlayerRace("Boar", 'B', 3, 2, 0);
        PlayerRace Fox = new PlayerRace("Fox", 'F', 2, 1, 2);
        PlayerRace Gecko = new PlayerRace("Gecko", 'G', 0, 0, 4);
        PlayerRace Raccoon = new PlayerRace("Raccoon", 'R', 2, 0, 3);
        PlayerRace Wolf = new PlayerRace("Wolf", 'W', 2, 2, 1);

        // Class initialization
        PlayerClass Knight = new PlayerClass("Knight", 'K', 3, 1, 0);
        PlayerClass Ranger = new PlayerClass("Ranger", 'R', 1, 1, 2);
        PlayerClass Shaman = new PlayerClass("Shaman", 'S', 2, 1, 1);
        PlayerClass Thief = new PlayerClass("Thief", 'T', 0, 0, 4);
        PlayerClass Warrior = new PlayerClass("Warrior", 'W', 2, 2, 0);

        // Monster initialization
        Monster Goblin = new Monster("Goblin", "a goblin", 3, 1, 1, 21);
        Monster Orc = new Monster("Orc", "an orc", 7, 2, 1, 48);
        Monster Troll = new Monster("Troll", "a troll", 11, 3, 2, 60);
        Monster Skeleton = new Monster("Skeleton", "a skeleton", 15, 2, 1, 75);
        Monster Zombie = new Monster("Zombie", "a zombie", 20, 2, 1, 100);
        Monster Wyrm = new Monster("Wyrm", "a wyrm", 25, 3, 3, 100);
        Monster Dragon = new Monster("Dragon", "a dragon", 40, 5, 3, 150);

        Dictionary <char, PlayerRace> raceDictionary = new Dictionary <char, PlayerRace>();
        Dictionary <char, PlayerClass> classDictionary = new Dictionary <char, PlayerClass>();
        List<Monster> monsterList = new List<Monster>{};

        List<string> adventureLog = new List<string>{};

        Monster activeMonster = Goblin;
        string enemyName = activeMonster.name;
        int enemyHealthCurrent = activeMonster.currentHealth;
        int enemyHealthMax = activeMonster.health;
        int enemyStrength = activeMonster.strength;
        int enemyAgility = activeMonster.agility;
        int enemyExperienceValue = activeMonster.experienceValue;

        InitializeProgram();
        CharacterCreation();
        ApplyPlayerStats();
        Refresh();
        adventureLog.Add($"Began your journey.");
        while (currentInstance <= maxInstances && playerIsAlive) Navigate();
        if (playerIsAlive) EndGame();

        void InitializeProgram()
        {
            Console.Clear();
            Console.WriteLine("====================");
            Console.WriteLine("PROGRAM START");
            Console.WriteLine("====================");
            Console.WriteLine();
            InitializeDictionaries();
        }


        void InitializeDictionaries()
        {
            raceDictionary.Add(Boar.key, Boar);
            raceDictionary.Add(Fox.key, Fox);
            raceDictionary.Add(Gecko.key, Gecko);
            raceDictionary.Add(Raccoon.key, Raccoon);
            raceDictionary.Add(Wolf.key, Wolf);

            classDictionary.Add(Knight.key, Knight);
            classDictionary.Add(Ranger.key, Ranger);
            classDictionary.Add(Shaman.key, Shaman);
            classDictionary.Add(Thief.key, Thief);
            classDictionary.Add(Warrior.key, Warrior);

            monsterList.Add(Goblin);
            monsterList.Add(Orc);
            monsterList.Add(Troll);
            monsterList.Add(Skeleton);
            monsterList.Add(Zombie);
            monsterList.Add(Wyrm);
            monsterList.Add(Dragon);
        }

        void DisplayPlayerStats()
        {
            Console.WriteLine();
            Console.WriteLine($"~~~~~~~~~~~~~~~");
            Console.WriteLine($"{playerName} the {playerRace.name} {playerClass.name}");
            Console.WriteLine($"Level {playerLevel} ({playerExperience} / {experienceToLevelUp})");
            Console.WriteLine($"- - - - - - - -");
            Console.WriteLine($"{playerHealthCurrent} / {playerHealthMax} HP");
            Console.WriteLine($"STR: {playerStrength} | AGI: {playerAgility}");
            Console.WriteLine($"~~~~~~~~~~~~~~~");
        }

        void DisplayEnemyStats()
        {
            Console.WriteLine();
            Console.WriteLine("x x x x x x x x x x x x");
            if (!monsterIsAlive || activeMonster == null)
                Console.WriteLine();
            else
            {
                Console.WriteLine($"{enemyName}");
                Console.WriteLine($"{enemyHealthCurrent} / {enemyHealthMax} HP");
                Console.WriteLine($"STR: {enemyStrength} | AGI: {enemyAgility}");
                Console.WriteLine("x x x x x x x x x x x x");
                Console.WriteLine();
            }
        }

        void Refresh()
        {
            Console.Clear();
            DisplayPlayerStats();
            if (activeMonster != null) DisplayEnemyStats();
            Console.WriteLine();
        }

        void CharacterCreation()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Wild Order miniature roguelike.");
            CharacterName();
            CharacterRace();
            CharacterClass();
        }

        void CharacterName()
        {
            Console.WriteLine("Please name your character.");
            string playerEntry = Console.ReadLine();
            if (String.IsNullOrEmpty(playerEntry))
            {
                CharacterName();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Is your character named {playerEntry}? (Y/N)");


                bool responseNeeded;

                do
                {
                    responseNeeded = true;
                    ConsoleKeyInfo keyPress = Console.ReadKey();
                    char keyPressCaps = Char.ToUpper(keyPress.KeyChar);

                    switch (keyPressCaps)
                    {
                        case 'N':
                            CharacterName();
                            responseNeeded = false;
                            break;
                        case 'Y':
                            playerName = playerEntry;
                            responseNeeded = false;
                            break;
                        default:
                            break;
                    }
                } while (responseNeeded);
            }
        }

        void CharacterRace()
        {
            Console.Clear();
            Console.WriteLine($"Please pick a race for {playerName}.");
            Console.WriteLine();
            foreach (var element in raceDictionary)
            {
                Console.WriteLine($"{element.Key} - {element.Value.name}");
            }
            Console.WriteLine();
            ConsoleKeyInfo keyPress = Console.ReadKey();
            char playerEntry = Char.ToUpper(keyPress.KeyChar);
            if (raceDictionary.ContainsKey(playerEntry))
            {
                var playerSelection = raceDictionary[playerEntry];
                Console.Clear();
                Console.WriteLine($"Is {playerName} a(n) {playerSelection.name}? (Y/N)");

                bool responseNeeded;

                do
                {
                    responseNeeded = true;
                    ConsoleKeyInfo keyPress2 = Console.ReadKey();
                    char keyPress2Caps = Char.ToUpper(keyPress2.KeyChar);

                    switch (keyPress2Caps)
                    {
                        case 'N':
                            CharacterRace();
                            responseNeeded = false;
                            break;
                        case 'Y':
                            playerRace = playerSelection;
                            responseNeeded = false;
                            break;
                        default:
                            break;
                    }
                } while (responseNeeded);
            }
            else
                CharacterRace();
        }

        void CharacterClass()
        {
            Console.Clear();
            Console.WriteLine($"Please pick a class for {playerName} the {playerRace.name}.");
            Console.WriteLine();
            foreach (var element in classDictionary)
            {
                Console.WriteLine($"{element.Key} - {element.Value.name}");
            }
            Console.WriteLine();
            ConsoleKeyInfo keyPress = Console.ReadKey();
            char playerEntry = Char.ToUpper(keyPress.KeyChar);
            if (classDictionary.ContainsKey(playerEntry))
            {
                PlayerClass playerSelection = classDictionary[playerEntry];
                Console.Clear();
                Console.WriteLine($"Is {playerName} a(n) {playerSelection.name}? (Y/N)");

                bool responseNeeded;

                do
                {
                    responseNeeded = true;
                    ConsoleKeyInfo keyPress2 = Console.ReadKey();
                    char keyPress2Caps = Char.ToUpper(keyPress2.KeyChar);

                    switch (keyPress2Caps)
                    {
                        case 'N':
                            CharacterClass();
                            responseNeeded = false;
                            break;
                        case 'Y':
                            playerClass = playerSelection;
                            responseNeeded = false;
                            break;
                        default:
                            break;
                    }
                } while (responseNeeded);
            }
            else
                CharacterClass();
        }

        void ApplyPlayerStats()
        {
            playerLevel = 1;
            playerExperience = 0;

            playerHealthMax = playerBaseHealth + playerRace.health + playerClass.health;
            playerStrength = playerBaseStrength + playerRace.strength + playerClass.strength;
            playerAgility = playerBaseAgility + playerClass.agility + playerRace.agility;

            playerHealthCurrent = playerHealthMax;
        }

        void Navigate()
        {
            Refresh();

            var random1 = new Random();
            int index1 = random1.Next(monsterList.Count);
            Monster monster1 = monsterList[index1];

            var random2 = new Random();
            int index2 = random2.Next(monsterList.Count);
            Monster monster2 = monsterList[index2];

            Console.WriteLine($"There are two paths ahead.");
            Console.WriteLine($"The path on the left shows signs of {monster1.name} activity.");
            Console.WriteLine($"The path on the right shows signs of {monster2.name} activity.");
            Console.WriteLine($"Choose your direction.");
            Console.WriteLine($"L - Left");
            Console.WriteLine($"R - Right");


            Console.WriteLine();

            bool waitingForResponse;

            do
            {
                waitingForResponse = true;
                ConsoleKeyInfo keyPress = Console.ReadKey();
                char hitKey = Char.ToUpper(keyPress.KeyChar);

                switch (hitKey)
                {
                    case 'L':
                        Refresh();
                        Console.WriteLine("You head left.");
                        activeMonster = monster1;
                        waitingForResponse = false;
                        break;
                    case 'R':
                        Refresh();
                        Console.WriteLine("You head right.");
                        activeMonster = monster2;
                        waitingForResponse = false;
                        break;
                    default:
                        break;
                }
            } while (waitingForResponse);


            enemyName = activeMonster.name;
            enemyHealthCurrent = activeMonster.currentHealth;
            enemyHealthMax = activeMonster.health;
            enemyStrength = activeMonster.strength;
            enemyAgility = activeMonster.agility;
            enemyExperienceValue = activeMonster.experienceValue;

            currentInstance++;
            Encounter();
        }

        void Encounter()
        {
            monsterIsAlive = true;
            Refresh();            
            Console.WriteLine($"You face an enemy {enemyName}.");
            Console.ReadKey(true);
            CombatRound();
        }

        void CombatRound()
        {
            if (monsterIsAlive && enemyAgility > playerAgility)
                MonsterAttack();

            while (playerIsAlive && monsterIsAlive)
            {
                if (playerIsAlive)
                    PlayerAttack();
                if (monsterIsAlive)
                    MonsterAttack();
            }
        }

        void PlayerAttack()
        {
            Refresh();
            enemyHealthCurrent -= playerStrength;            
            Refresh();
            Console.WriteLine();
            Console.WriteLine($"You attack the enemy {enemyName}!");
            Console.WriteLine($"The attack causes {playerStrength} damage.");
            Console.ReadKey(true);
            if (enemyHealthCurrent <= 0)
            {
                enemyHealthCurrent = 0;
                EnemyDeath();
            }
        }

        void MonsterAttack()
        {
            playerHealthCurrent -= enemyStrength;    
            Refresh();        
            Console.WriteLine();
            Console.WriteLine($"The enemy {enemyName} attacks!");
            Console.WriteLine($"The attack causes {enemyStrength} damage.");
            Console.ReadKey(true);
            if (playerHealthCurrent <= 0)
            {
                playerHealthCurrent = 0;
                PlayerDeath();
            }
        }

        void PlayerDeath()
        {
            Refresh();
            Console.WriteLine("You die...");
            adventureLog.Add($"Killed by {enemyName}.");
            playerIsAlive = false;
            Console.ReadKey(true);
            DisplayAdventureLog();
        }

        void EnemyDeath()
        {
            monsterIsAlive = false;            
            playerExperience += enemyExperienceValue;
            Refresh();            
            Console.WriteLine($"The enemy {enemyName} is killed!");
            Console.WriteLine($"You gain {enemyExperienceValue} XP.");
            Console.ReadKey(true);
            adventureLog.Add($"Defeated {enemyName}.");
            if (playerExperience >= experienceToLevelUp)
                LevelUp();
        }

        void EndGame()
        {
            Refresh();
            Console.WriteLine($"You have reached the end of your journey.");
            adventureLog.Add($"Finished your journey.");
            Console.ReadKey(true);
            DisplayAdventureLog();
        }

        void DisplayAdventureLog()
        {
            Console.Clear();

            foreach (var entry in adventureLog)
            {
                Console.WriteLine(entry);
            }
        }

        void LevelUp()
        {
            playerLevel++;
            playerExperience -= experienceToLevelUp;
            experienceToLevelUp = (int)Math.Round(experienceToLevelUp * experienceMultiplier, 0);
            Refresh();            
            Console.WriteLine($"You have gained enough experience to level up!");
            Console.WriteLine($"You are now level {playerLevel}.");
            Console.ReadKey(true);
            var random1 = new Random();
            var random2 = new Random();
            var random3 = new Random();
            int healthBoost = random1.Next(healthIncreaseMin, healthIncreaseMax);
            int strengthBoost = random2.Next(statIncreaseMin, statIncreaseMax);
            int agilityBoost = random3.Next(statIncreaseMin, statIncreaseMax);

            playerHealthMax += healthBoost;
            playerHealthCurrent += healthBoost;
            playerStrength += strengthBoost;
            playerAgility += agilityBoost;

            Refresh();
            Console.WriteLine($"You gain {healthBoost} HP.");
            if (strengthBoost > 0) Console.WriteLine($"You gain {strengthBoost} strength.");
            if (agilityBoost > 0) Console.WriteLine($"You gain {agilityBoost} agility.");

            Console.ReadKey(true);

            adventureLog.Add($"Reached level {playerLevel}.");

            if (playerExperience >= experienceToLevelUp) LevelUp();
        }
    }
}

public class PlayerRace
{
    public string name;
    public char key;
    public int health;
    public int strength;
    public int agility;

    // Constructor
    public PlayerRace (string name, char key, int health, int strength, int agility)
    {
        this.name = name;
        this.key = key;
        this.health = health;
        this.strength = strength;
        this.agility = agility;
    }
}

public class PlayerClass
{
    public string name;
    public char key;
    public int health;
    public int strength;
    public int agility;

    // Constructor
    public PlayerClass (string name, char key, int health, int strength, int agility)
    {
        this.name = name;
        this.key = key;
        this.health = health;
        this.strength = strength;
        this.agility = agility;
    }
}

public class Monster
{
    public string name;
    public string properName;
    public int health;
    public int strength;
    public int agility;
    public int experienceValue;

    public int currentHealth;

    public Monster (string name, string properName, int health, int strength, int agility, int experienceValue)
    {
        this.name = name;
        this.properName = properName;
        this.health = health;
        this.strength = strength;
        this.agility = agility;
        this.experienceValue = experienceValue;

        this.currentHealth = health;
    }
}