int maxHealth = 25;
int currentHealth;
int minimumDamage = 1;
int maximumDamage = 10;
float hitChance = 75;

int healing = 10;

string enemyName = "squirrel";

currentHealth = maxHealth;

bool TryAttack()
{
    var random = new Random();
    int hitRoll = random.Next(0, 100);

    if (hitRoll <= hitChance)
        return true;
    else
        return false;
}

int HitDamage()
{
    var random = new Random();
    int damageAmount = random.Next(minimumDamage, maximumDamage);
    return damageAmount;
}

void MakeAttack()
{
    Console.ReadKey(true);
    Console.WriteLine("The enemy " + enemyName + " makes an attack!");
    bool doesAttackHit = TryAttack();
    if (doesAttackHit == false)
    {
        Console.WriteLine("The attack misses!");
        Console.WriteLine();
        MakeAttack();
    }
    else
    {
        Console.WriteLine("The attack hits!");
        int hitDamage = HitDamage();
        Console.WriteLine("The attack causes " + hitDamage + " points of damage.");
        currentHealth -= hitDamage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Console.WriteLine("You are defeated...");
        }
        else
        {
            Console.WriteLine("Hit points left: " + currentHealth);
            Console.WriteLine();
            Console.WriteLine("Press H to heal.");

            ConsoleKeyInfo keyPress = Console.ReadKey(true);
            char hitKey = Char.ToUpper(keyPress.KeyChar);

            if (hitKey == 'H')
            {
                Healing();
                MakeAttack();
            }
            else MakeAttack();
        }
        
    }
}

void Healing()
{
    Console.WriteLine($"You heal {healing} points.");
    currentHealth += healing;
}

Console.Clear();
MakeAttack();