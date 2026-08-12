using System;
using System.Globalization;
using System.Linq;

public class Program
{
    char[] letters = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

    // Playable races ======================================================================================
    Creature Human = new Creature("Human", 10, 2, 2, 2, true, "Humans are adaptable and versatile, and more well-rounded than other races, though they also generally don't exceed the feats of those stronger, faster, or smarter than them.");
    Creature Orc = new Creature("Orc", 12, 4, 2, 1, true, "Orcs are fearsome and strong, and while their abilities of reasoning and perception are lacking, they're some of the most ferocious warriors in the world.");
    Creature Elf = new Creature("Elf", 9, 1, 2, 4, true, "Elves thrive when their tactical and perceptible minds can flex, and they're unmatched as scouts, thieves, and archers.");
    Creature Dwarf = new Creature("Dwarf", 15, 3, 1, 1, true, "Dwarves are a stocky and hardy race, and they're sturdier than other species found in the world.");

    // Unplayable races ======================================================================================
    Creature Wolf = new Creature("Wolf", 5, 3, 3, 3, false, "A wolf.");
    Creature Sahuagin = new Creature("Sahuagin", 7, 2, 3, 1, false, "A sahuagin.");
    Creature Naga = new Creature("Naga", 10, 2, 4, 1, false, "A naga.");
    Creature Wyrm = new Creature("Wyrm", 12, 3, 2, 2, false, "A wyrm.");
    Creature Goblin = new Creature("Goblin", 5, 2, 2, 1, false, "A goblin.");
    Creature Dragon = new Creature("Dragon", 30, 10, 8, 7, false, "A dragon.");
    Creature Giant = new Creature("Giant", 20, 9, 5, 5, false, "A giant.");
    Creature Troll = new Creature("Troll", 15, 6, 3, 4, false, "A troll.");

    // Character classes ======================================================================================
    CharacterClass Warrior = new CharacterClass("Warrior", 5, 3, 1, 0);
    CharacterClass Rogue = new CharacterClass("Rogue", 2, 0, 3, 1);
    CharacterClass Hunter = new CharacterClass("Hunter", 1, 1, 2, 3);

    // Lists ======================================================================================
    List<Creature> creatureList = new List<Creature>(); // All creatures
    List<Creature> playableList = new List<Creature>(); // Playable and classable creatures
    List<CharacterClass> classList = new List<CharacterClass>(); // Playable classes
    List<Creature> enemyPool = new List<Creature>(); // Level-appropriate selection of enemies, updated each encounter
    List<string> journal = new List<string>();

    // Player data ======================================================================================
    string playerName = null;
    string playerRace = null;
    string playerClass = null;
    int playerLevel = 0;
    int playerExperience = 0;
    int experienceToLevelUp = 0;

    Creature playerRaceData = null;
    CharacterClass playerClassData = null;

    int playerCurrentHealth = 0;
    int playerMaxHealth = 0;
    int playerStrength = 0;
    int playerAgility = 0;
    int playerPerception = 0;

    // Leveling variables ======================================================================================
    float experienceMultiplier = 1.33f;
    int startingExperienceRequirement = 100;
    int maxLevel = 20;

    int healthPerLevelMin = 1;
    int healthPerLevelMax = 3;
    int statPerLevelMin = 0;
    int statPerLevelMax = 2;

    List<int> experienceRequirements = new List<int>();

    int healthExpValue = 3;
    int strengthExpValue = 7;
    int agilityExpValue = 6;
    int perceptionExpValue = 5;

    // Enemy data ======================================================================================
    Creature enemyData = null;
    
    string enemyName = null;
    int enemyCurrentHealth = 0;
    int enemyMaxHealth = 0;
    int enemyStrength = 0;
    int enemyAgility = 0;
    int enemyPerception = 0;
    int experienceValue = 0;

    // Game flow variables ======================================================================================
    int day = 0;
    int round = 0;
    bool isInCombat = false;
    bool isPlayerAlive = true;
    bool isEnemyAlive = true;
    float criticalMultiplier = 1.5f;
    float dailyRegeneration = 0.2f; // 1f = 100%; amount of max health regenerated each day
    int skippableDays = 3; // maximum number of times the player can skip a battle
    int currentSkippableDays;
    bool isPlayerTurn = true;

    // Main =================================================================================================================================
    public static void Main (String[] args)
    {
        Program program = new Program();
        // Initialization
        program.InitializeGame();
        program.CharacterCreation();
        program.InitializePlayer();
        Console.Clear();
        program.Refresh(true);
        while (program.isPlayerAlive)
        {
            program.NewDay();
            program.Encounter(program.enemyData);
        }
    }

    // Methods ======================================================================================
    void Wait(int duration)
    {
        System.Threading.Thread.Sleep(duration);
    }

    void PressAnyKey()
    {
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey(true);       
    }

    void Encounter(Creature enemy)
    {
        Refresh(true);
        Console.WriteLine($"On day {day}, your opponent will be {enemy.name}.");
        Console.WriteLine();
        if (currentSkippableDays > 0)
        {
            bool confirmationRequired = true;
            while (confirmationRequired)
            {
                Console.WriteLine($"Will you fight the {enemy.name}? (You can skip {currentSkippableDays} more fights.) Y/N");
                ConsoleKeyInfo keyPress = Console.ReadKey(true);
                char key = Char.ToUpper(keyPress.KeyChar);

                switch(key)
                {
                    case 'Y':
                        confirmationRequired = false;
                        Combat(enemy);
                        break;
                    case 'N':
                        confirmationRequired = false;
                        Refresh(true);
                        Console.WriteLine($"You decide to rest and wait until tomorrow.");
                        currentSkippableDays--;
                        if (currentSkippableDays < 0) currentSkippableDays = 0;
                        PressAnyKey();
                        break;
                }
            }
        }
        else
        {
            PressAnyKey();
            Combat(enemy);
        }
    }

    void Combat(Creature enemy)
    {
        Refresh(true);
        round = 1;
        isInCombat = true;
        Console.WriteLine($"You fight the {enemy.name}.");
        bool playerInitiative = PlayerInitiative(enemy);
        if (playerInitiative)
        {
            Console.WriteLine($"You have the initiative.");
            isPlayerTurn = true;
        }
        else
        {
            Console.WriteLine($"The enemy has the initiative.");
            isPlayerTurn = false;
        }
        Console.WriteLine();
        PressAnyKey();
        while (isPlayerAlive && isEnemyAlive)
        {
            CombatRound(enemy);
        }
    }

    void PreventNegativeHealth()
    {
            if (playerCurrentHealth <= 0)
            {
                playerCurrentHealth = 0;
                isPlayerAlive = false;
            }

            if (enemyCurrentHealth <= 0)
            {
                enemyCurrentHealth = 0;
                isEnemyAlive = false;
            }        
    }

    void CombatRound(Creature enemy)
    {
        Refresh(true);
        string attackMessage;
        int attackDamage;
        int criticalDamage;

        if (isPlayerTurn)
        {
            attackDamage = playerStrength;
            criticalDamage = Convert.ToInt32(playerStrength * criticalMultiplier);
        }
        else
        {
            attackDamage = enemy.strength;
            criticalDamage = Convert.ToInt32(enemy.strength * criticalMultiplier);
        }

        Wait(100);

        if (!AttackRoll(enemy))
        {
            attackMessage = isPlayerTurn ? $"You attack the {enemy.name}." : $"The {enemy.name} attacks!";
            Console.WriteLine(attackMessage);
            Console.WriteLine($"The attack misses!");
        }
        else
        {
            if (!CriticalRoll(enemy))
            {
                if (isPlayerTurn)
                {
                    enemyCurrentHealth -= attackDamage;
                }
                else
                {
                    playerCurrentHealth -= attackDamage;
                }
                PreventNegativeHealth();    
                Refresh(true);
                attackMessage = isPlayerTurn ? $"You attack the {enemy.name}." : $"The {enemy.name} attacks!";
                Console.WriteLine(attackMessage);
                Console.WriteLine($"The attack hits! {attackDamage} damage.");
            }
            else
            {
                if (isPlayerTurn)
                {
                    enemyCurrentHealth -= criticalDamage;
                }
                else
                {
                    playerCurrentHealth -= criticalDamage;
                }
                PreventNegativeHealth();
                Refresh(true);
                attackMessage = isPlayerTurn ? $"You attack the {enemy.name}." : $"The {enemy.name} attacks!";
                Console.WriteLine(attackMessage);
                Console.WriteLine($"A critical hit! {criticalDamage} damage!");                
            }

            if (playerCurrentHealth <= 0)
            {
                playerCurrentHealth = 0;
                isPlayerAlive = false;
                PlayerDeath();
            }

            if (enemyCurrentHealth <= 0)
            {
                enemyCurrentHealth = 0;
                isEnemyAlive = false;
                PlayerVictory(enemy);
            }
        }

        if (isPlayerAlive && isEnemyAlive) PressAnyKey();
        isPlayerTurn = !isPlayerTurn;
    }

    void PlayerDeath()
    {
        Console.WriteLine($"You have died...");
        PressAnyKey();
    }

    void PlayerVictory(Creature enemy)
    {
        isInCombat = false;
        Console.WriteLine($"You defeated the {enemy.name}!");
        if (playerLevel < maxLevel)
        {
            Console.WriteLine($"You gain {enemy.experienceValue} experience.");
            playerExperience += enemy.experienceValue;
        }
        PressAnyKey();
        while (playerExperience >= experienceToLevelUp) LevelUp();
    }

    void LevelUp()
    {
        Refresh(true);
        if (playerLevel == maxLevel)
        {
            Console.WriteLine($"You are already at max level.");
            PressAnyKey();
        }
        else
        {
            playerExperience -= experienceToLevelUp;
            playerLevel++;
            experienceToLevelUp = experienceRequirements[playerLevel];
            Refresh(true);
            Console.WriteLine($"*** You have reached level {playerLevel}! ***");
            PressAnyKey();

            var random1 = new Random();
            var random2 = new Random();
            var random3 = new Random();
            var random4 = new Random();

            int healthBonus = random1.Next(healthPerLevelMin, healthPerLevelMax+1);
            int strengthBonus = random2.Next(statPerLevelMin, statPerLevelMax+1);
            int agilityBonus = random3.Next(statPerLevelMin, statPerLevelMax+1);
            int perceptionBonus = random4.Next(statPerLevelMin, statPerLevelMax+1);

            playerMaxHealth += healthBonus;
            playerCurrentHealth += healthBonus;
            playerStrength += strengthBonus;
            playerAgility += agilityBonus;
            playerPerception += perceptionBonus;

            Refresh(true);

            if (healthBonus > 0)
                Console.WriteLine($"You gain {healthBonus} HP.");
            if (strengthBonus > 0)
                Console.WriteLine($"You gain {strengthBonus} STR.");
            if (agilityBonus > 0)
                Console.WriteLine($"You gain {agilityBonus} AGI.");
            if (perceptionBonus > 0)
                Console.WriteLine($"You gain {perceptionBonus} PER.");

            Console.WriteLine();

            PressAnyKey();
        }
    }

    bool AttackRoll(Creature enemy)
    {
        int attackPool = playerAgility + enemy.agility + playerPerception + enemy.perception;
        var random = new Random();
        int attackRoll = random.Next(attackPool);
        if (isPlayerTurn)
        {
            if (attackRoll <= playerAgility) return false;
            else return true;
        }
        else
        {
            if (attackRoll <= enemy.agility) return false;
            else return true;
        }
    }

    bool CriticalRoll(Creature enemy)
    {
        int criticalPool = playerAgility + enemy.agility + playerPerception + enemy.perception;
        var random = new Random();
        int criticalRoll = random.Next(criticalPool);
        if (isPlayerTurn)
        {
            if (criticalRoll <= playerPerception) return true;
            else return false;
        }
        else
        {
            if (criticalRoll <= enemy.perception) return true;
            else return false;
        }
    }

    bool PlayerInitiative(Creature enemy)
    {
        int initiativePool = playerAgility + enemy.agility;
        var random = new Random();
        int initiativeRoll = random.Next(initiativePool);
        if (initiativeRoll <= playerAgility) return true;
        else return false;
    }

    int RegenerateHealth(int health)
    {
        float amountToRegenerate = health * dailyRegeneration;
        int amountToRegenerateInt = Convert.ToInt32(amountToRegenerate);
        health += amountToRegenerateInt;
        if (health >= playerMaxHealth) health = playerMaxHealth;
        return health;
    }

    void NewDay()
    {
        day++;
        playerCurrentHealth = RegenerateHealth(playerCurrentHealth);
        GenerateEnemy();
        Refresh(true);
        Console.WriteLine($"It is now day {day}.");
        PressAnyKey();
    }

    void InitializeGame()
    {
        Console.Clear();
        Wait(100);

        Console.WriteLine($"===| Populating lists...");
        Wait(100);
        PopulateLists();

        Console.WriteLine($"===| Calculating levels...");
        Wait(100);
        CalculateLevels();

        Console.WriteLine($"===| Calculating experience values...");
        Wait(100);
        CalculateExperienceValues();

        Console.WriteLine($"===| Launching intro...");
        Wait(500);
        Intro();
    }

    void Intro()
    {
        Console.Clear();
        Wait(75);
        Console.WriteLine("======================================================");
        Wait(75);
        Console.WriteLine("X====================================================X");
        Wait(75);
        Console.WriteLine("+X==================================================X+");
        Wait(75);
        Console.WriteLine("++X============|                      |============X++");
        Wait(75);
        Console.WriteLine("+++X==========|  A R E N A    D U E L  |==========X+++");
        Wait(75);
        Console.WriteLine("++X============|                      |============X++");
        Wait(75);
        Console.WriteLine("+X==================================================X+");
        Wait(75);
        Console.WriteLine("X====================================================X");
        Wait(75);
        Console.WriteLine("======================================================");
        Wait(75);
        Console.WriteLine();
        Console.WriteLine();
        Console.Write("Press ");
        Wait(25);
        Console.Write("any ");
        Wait(25);
        Console.Write("key ");
        Wait(25);
        Console.Write("to ");
        Wait(25);
        Console.Write("continue.");

        Console.ReadKey(true);
    }

    void DisplayChargenHeader()
    {
        Console.Clear();
        Console.WriteLine("============================");
        Console.WriteLine("===| CHARACTER CREATION |===");
        Console.WriteLine("============================");
        Console.WriteLine();
    }

    void DisplayPlayerCard()
    {
        Console.WriteLine($"=====================================");
        Console.WriteLine($"==| {playerName.ToUpper(new CultureInfo("en-US", false))}");
        Console.WriteLine($"==| {playerRace} {playerClass} - Level {playerLevel} ({playerExperience} / {experienceToLevelUp})");
        Console.WriteLine("==| ");
        Console.WriteLine($"==| {playerCurrentHealth}/{playerMaxHealth} HP - STR {playerStrength} - AGI {playerAgility} - PER {playerPerception}");
        Console.WriteLine($"=====================================");
        Console.WriteLine();
    }
    
    void DisplayEnemyCard()
    {
        Console.WriteLine($"=====================================");
        Console.WriteLine($"==| {enemyName.ToUpper(new CultureInfo("en-US", false))}");
        Console.WriteLine("==| ");
        Console.WriteLine($"==| {enemyCurrentHealth}/{enemyMaxHealth} HP");
        Console.WriteLine($"=====================================");
        Console.WriteLine();
    }

    void DisplayGameInfo()
    {
        Console.Write($"==| Day {day}");
        if (isInCombat) Console.Write($" | Round {round}");
        Console.WriteLine();
    }

    void Refresh(bool doClear)
    {
        if (doClear) Console.Clear();
        DisplayGameInfo();
        DisplayPlayerCard();
        if (isInCombat) DisplayEnemyCard();
    }

    void CharacterCreation()
    {
        bool characterConfirmed = false;
        while (!characterConfirmed)
        {
            bool entryRequired = true;
            while (entryRequired)
            {
                DisplayChargenHeader();
                Console.WriteLine($"Please name your character.");
                Console.WriteLine();

                string nameEntry = Console.ReadLine();
                if (String.IsNullOrEmpty(nameEntry)) continue;
                else
                {
                    bool confirmationRequired = true;
                    DisplayChargenHeader();
                    Console.WriteLine($"Is {nameEntry} the name of your character? Y/N");
                    Console.WriteLine();

                    ConsoleKeyInfo keyPress = Console.ReadKey(true);
                    char key = Char.ToUpper(keyPress.KeyChar);
                    switch (key)
                    {
                        case 'Y':
                            entryRequired = false;
                            confirmationRequired = false;
                            playerName = nameEntry;
                            break;
                        case 'N':
                            confirmationRequired = false;
                            break;
                    }
                }
            }

            CharacterRaceSelection();
            CharacterClassSelection();
            characterConfirmed = ConfirmCharacter();
        }
    }

    void CharacterRaceSelection()
    {
        bool entryRequired = true;
        while (entryRequired)
        {
            DisplayChargenHeader();
            Console.WriteLine($"Please pick a race for {playerName}.");
            Console.WriteLine();
            Wait(25);
            foreach (var entry in playableList)
            {
                Console.WriteLine($"{letters[playableList.IndexOf(entry)]} - {entry.name}");
                Wait(25);
            }

            ConsoleKeyInfo keyPress = Console.ReadKey(true);
            char key = Char.ToUpper(keyPress.KeyChar);
            if (!letters.Contains(key) || letters.IndexOf(key) >= playableList.Count) continue;
            else
            {
                bool confirmationRequired = true;
                DisplayChargenHeader();
                int selectionIndex = letters.IndexOf(key);
                var selection = playableList[selectionIndex];
                Console.WriteLine($"Is {playerName} a(n) {selection.name}? Y/N");
                Console.WriteLine();
                Console.WriteLine(selection.description);
                Console.WriteLine();

                ConsoleKeyInfo keyPress2 = Console.ReadKey(true);
                char key2 = Char.ToUpper(keyPress2.KeyChar);
                switch(key2)
                {
                    case 'Y':
                        entryRequired = false;
                        confirmationRequired = false;
                        playerRaceData = selection;
                        playerRace = selection.name;
                        break;
                    case 'N':
                        confirmationRequired = false;
                        break;
                }
            }
        }
    }

    void CharacterClassSelection()
    {
        bool entryRequired = true;
        while (entryRequired)
        {
            DisplayChargenHeader();
            Console.WriteLine($"Please pick a class for {playerName} the {playerRace}.");
            Console.WriteLine();
            Wait(25);
            foreach (var entry in classList)
            {
                Console.WriteLine($"{letters[classList.IndexOf(entry)]} - {entry.name}");
                Wait(25);
            }

            ConsoleKeyInfo keyPress = Console.ReadKey(true);
            char key = Char.ToUpper(keyPress.KeyChar);
            if (!letters.Contains(key) || letters.IndexOf(key) >= classList.Count) continue;
            else
            {
                bool confirmationRequired = true;
                DisplayChargenHeader();
                int selectionIndex = letters.IndexOf(key);
                var selection = classList[selectionIndex];
                Console.WriteLine($"Is {playerName} a(n) {selection.name}? Y/N");
                Console.WriteLine();

                ConsoleKeyInfo keyPress2 = Console.ReadKey(true);
                char key2 = Char.ToUpper(keyPress2.KeyChar);
                switch(key2)
                {
                    case 'Y':
                        entryRequired = false;
                        confirmationRequired = false;
                        playerClassData = selection;
                        playerClass = selection.name;
                        break;
                    case 'N':
                        confirmationRequired = false;
                        break;
                }
            }
        }
    }

    bool ConfirmCharacter()
    {
        DisplayChargenHeader();
        Console.WriteLine($"Is this your character? Y/N");
        Console.WriteLine();
        Console.WriteLine($"{playerName} the {playerRace} {playerClass}");

        ConsoleKeyInfo keyPress = Console.ReadKey(true);
        char key = Char.ToUpper(keyPress.KeyChar);

        switch (key)
        {
            case 'Y':
                return true;
                break;
            case 'N':
                return false;
                break;
        }

        return false;
    }

    void InitializePlayer()
    {
        playerMaxHealth = playerRaceData.health + playerClassData.health;
        playerCurrentHealth = playerMaxHealth;
        playerStrength = playerRaceData.strength + playerClassData.strength;
        playerAgility = playerRaceData.agility + playerClassData.agility;
        playerPerception = playerRaceData.perception + playerClassData.perception;
        playerLevel = 1;
        playerExperience = 0;
        experienceToLevelUp = experienceRequirements[playerLevel];
        currentSkippableDays = skippableDays;
    }

    void GenerateEnemy()
    {
        PopulateEnemyPool();
        enemyData = null;
        var random = new Random();
        int index = random.Next(enemyPool.Count);
        enemyData = enemyPool[index];
        isEnemyAlive = true;
        InitializeEnemy(enemyData);
    }

    void InitializeEnemy(Creature enemy)
    {
        enemyName = enemy.name;
        enemyMaxHealth = enemy.health;
        enemyCurrentHealth = enemyMaxHealth;
        enemyStrength = enemy.strength;
        enemyAgility = enemy.agility;
        enemyPerception = enemy.perception;
        experienceValue = enemy.experienceValue;        
    }

    void CalculateLevels()
    {
        experienceRequirements.Add(0);
        int experience = startingExperienceRequirement;
        experienceRequirements.Add(experience);
        Console.WriteLine($"Level 1 calculated ({experience})");
        Wait(25);

        for (int i = 2; i < maxLevel; i++)
        {
            experience = Convert.ToInt32(experience * experienceMultiplier);
            experienceRequirements.Add(experience);
            Console.WriteLine($"Level {i} calculated ({experience})");
            Wait(25);
        }
    }

    void CalculateExperienceValues()
    {
        foreach (var entry in creatureList)
        {
            entry.experienceValue = ExperienceValue(entry.health, entry.strength, entry.agility, entry.perception);
            if (entry.isPlayable)
            {
                entry.experienceValue *= 2;
            }

            Console.WriteLine($"Calculated experience value for {entry.name} ({entry.experienceValue})");
            Wait(25);
        }
    }

    int ExperienceValue(int health, int strength, int agility, int perception)
    {
        int result =
        (healthExpValue * health) +
        (strengthExpValue * strength) +
        (agilityExpValue * agility) +
        (perceptionExpValue * perception);

        return result;
    }

    void PopulateEnemyPool()
    {
        enemyPool.Clear();

        foreach (var entry in creatureList)
        {
            if (entry.experienceValue <= experienceToLevelUp)
            {
                enemyPool.Add(entry);
            }
        }
    }

    void PopulateLists()
    {
        // Clear existing lists to avoid duplication
        creatureList.Clear();
        playableList.Clear();
        journal.Clear();
        Console.WriteLine($"Lists cleared for initialization...");
        Wait(50);

        // Populate main creature library
        creatureList.Add(Human);
        creatureList.Add(Orc);
        creatureList.Add(Elf);
        creatureList.Add(Dwarf);
        creatureList.Add(Wolf);
        creatureList.Add(Sahuagin);
        creatureList.Add(Naga);
        creatureList.Add(Wyrm);
        creatureList.Add(Goblin);
        creatureList.Add(Dragon);
        creatureList.Add(Giant);
        creatureList.Add(Troll);

        // Automatically populate playable race library
        foreach (var entry in creatureList)
        {
            Console.WriteLine($"{entry.name} added to creature list.");
            if (entry.isPlayable)
            {
                playableList.Add(entry);
                Console.WriteLine($"{entry.name} added to playable list.");
            }
            Wait(50);
        }

        // Populate class library
        classList.Add(Warrior);
        classList.Add(Rogue);
        classList.Add(Hunter);

        foreach (var entry2 in classList)
        {
            Console.WriteLine($"{entry2.name} added to class list.");
            Wait(50);
        }
    }
}

