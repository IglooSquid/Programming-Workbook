using System;

class Program
{
    int percentageMin = 0;
    int percentageMax = 100;

    string playerName = "Player";
    string enemyName = "Enemy";

    bool playerIsAlive = true;

    int playerHealth = 100;

    public static void Main(String[] args)
    {
        var program = new Program();

        Weapon Broadsword = new Weapon("Broadsword", 25, 10, 25);

        while (program.playerIsAlive)
            program.EnemyAttack(Broadsword.name, Broadsword.baseDamage, Broadsword.damageVariance, Broadsword.criticalChance);
    }

    public void EnemyAttack(string weaponName, int damage, int damageVariance, int criticalChance)
    {
        Console.WriteLine();
        Console.WriteLine("* * * * * * * * * * * *");
        Console.WriteLine();
        Console.WriteLine($"{enemyName} attacks with a {weaponName}!");
        int attackDamage = CalculateDamage(damage, damageVariance);
        Console.WriteLine($"Checking critical hit with critical chance {criticalChance}%...");
        bool criticalHit = CheckCritical(criticalChance);
        if (criticalHit)
        {
            Console.WriteLine("It's a critical hit!");
            attackDamage *= 2;
        }
        else
        {
            Console.WriteLine("Normal hit.");
        }

        Console.WriteLine($"The hit deals {attackDamage} points of damage.");
        ReduceHealth(attackDamage);
    }

    public bool CheckCritical(int criticalChance)
    {
        var random = new Random();
        int randomRoll = random.Next(percentageMin, percentageMax);
        Console.WriteLine($"Rolled a {randomRoll} against a {criticalChance}% critical chance.");
        bool isCritical = randomRoll <= criticalChance ? true : false;
        return isCritical;
    }

    public int CalculateDamage(int damage, int damageVariance)
    {
        int minimumDamage = damage - damageVariance;
        int maximumDamage = damage + damageVariance;

        var random = new Random();
        int damageOutput = random.Next(minimumDamage, maximumDamage);

        return damageOutput;
    }

    public void ReduceHealth(int damageDealt)
    {
        for (int i = damageDealt; i > 0; i--)
        {
            playerHealth--;
            if (playerHealth <= 0)
            {
                PlayerDeath();
                break;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"{playerHealth} HP remaining.");

    }

    public void PlayerDeath()
    {
        Console.WriteLine($"{playerName} has died...");
        playerIsAlive = false;
    }    
}

public class Weapon
{
    public string name;
    public int baseDamage;
    public int damageVariance;
    public int criticalChance;

    public Weapon (string name, int baseDamage, int damageVariance, int criticalChance)
    {
        this.name = name;
        this.baseDamage = baseDamage;
        this.damageVariance = damageVariance;
        this.criticalChance = criticalChance;
    }
}