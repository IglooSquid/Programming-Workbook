using System;
using System.Collections.Generic;

Monster activeMonsterObject = null;
string activeMonster = "None";

int criticalChanceBase = 5;
float criticalDamageMultiplier = 1.5f;

int currentCriticalChance = 0;

Monster Goblin = new Monster ("Goblin", 5, 7, 2);
Monster Orc = new Monster ("Orc", 8, 18, 5);
Monster Sahuagin = new Monster ("Sahuagin", 10, 20, 15);
Monster Drake = new Monster ("Drake", 25, 35, 10);

List<Monster> monsterList = new List<Monster>();

monsterList.Add(Goblin);
monsterList.Add(Orc);
monsterList.Add(Sahuagin);
monsterList.Add(Drake);

char[] letters = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'};

MonsterSelect();
Simulation();

void Refresh()
{
    Console.Clear();
    Console.WriteLine($"Active monster: {activeMonster}");
    Console.WriteLine("- - - - -");
    Console.WriteLine();
}

void MonsterSelect()
{
    Refresh();
    Console.WriteLine($"Select active monster.");
    Console.WriteLine();

    foreach (var entry in monsterList)
    {
        int index = monsterList.IndexOf(entry);
        Console.WriteLine($"{letters[index]} - {entry.name}");
    }

    bool inputRequired = true;

    while (inputRequired)
    {
        ConsoleKeyInfo keyPress = Console.ReadKey(true);
        char key = Char.ToUpper(keyPress.KeyChar);

        if (!letters.Contains(key)) continue;

        int index = Array.IndexOf(letters, key);

        if (index < monsterList.Count)
        {
            Refresh();
            inputRequired = false;
            var selection = monsterList[index];
            activeMonster = selection.name;
            Console.WriteLine($"{activeMonster} selected.");
            activeMonsterObject = selection;
        }
    }

    CalculateCritical(activeMonsterObject);
}

int CalculateDamage(int min, int max)
{
    var random = new Random();
    int damage = random.Next(min, max);
    return damage;
}

void CalculateCritical(Monster monster)
{
    currentCriticalChance = monster.criticalChance + criticalChanceBase;
}

bool AttackRoll(float criticalChance)
{
    var random = new Random();
    int attackRoll = random.Next(0, 100);
    Console.WriteLine($"Rolled {attackRoll} versus {criticalChance}.");
    if (attackRoll < criticalChance) return true;
    else return false;
}

void Simulation()
{
    Refresh();
    Console.WriteLine($"Making an attack roll for {activeMonster} with critical chance {currentCriticalChance}.");
    int damage = CalculateDamage(activeMonsterObject.attackDamageMin, activeMonsterObject.attackDamageMax);
    Console.WriteLine($"Attack damage: {damage}.");
    bool critical = AttackRoll(currentCriticalChance);
    if (critical)
    {
        damage = Convert.ToInt32(damage * criticalDamageMultiplier);
        Console.WriteLine($"Critical hit!");
    }
    Console.WriteLine($"{damage} damage.");

    Console.WriteLine();
    Console.WriteLine($"Press A to simulate again.");
    Console.WriteLine($"Press B to return to monster selection.");
    Console.WriteLine();

    bool inputRequired = true;
    while (inputRequired)
    {
        ConsoleKeyInfo keyPress = Console.ReadKey(true);
        char key = Char.ToUpper(keyPress.KeyChar);
        if (key != 'A' && key != 'B') continue;
        switch (key)
        {
            case 'A':
                Simulation();
                break;
            case 'B':
                activeMonsterObject = null;
                activeMonster = "None";
                MonsterSelect();
                break;
        }
    }
}