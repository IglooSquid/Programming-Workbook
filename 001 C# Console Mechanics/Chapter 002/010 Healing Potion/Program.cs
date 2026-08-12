int potionStock = 3;
int healingAmount = 10;
int playerHealthMax = 100;
int playerHealthCurrent;

int minimumPotions = 0; // for later adding manual adjustment of stock
int maximumPotions = 5; // for later adding manual adjustment of stock

int damageBase = 15;
int damageVariance = 5;

bool playerIsAlive = true;

playerHealthCurrent = playerHealthMax;

while (playerIsAlive) TakeDamage();

void TakeDamage()
{
    Console.WriteLine();
    Console.WriteLine("**********");
    Console.WriteLine();
    var random = new Random();
    int variance = random.Next(-damageVariance, damageVariance);
    int damage = damageBase + variance;
    Console.WriteLine($"You take {damage} points of damage.");
    for (int i = damage; i > 0; i--)
    {
        playerHealthCurrent--;
        if (playerHealthCurrent <= 0)
        {
            playerHealthCurrent = 0;
            Console.WriteLine($"You are defeated...");
            playerIsAlive = false;
            break;
        }
    }
    Console.WriteLine($"{playerHealthCurrent} HP left.");
    CheckHealing();
}

void CheckHealing()
{
    if (potionStock <= 0)
    {
        Console.WriteLine($"No more healing potions left.");
    }
    else
    {
        Console.WriteLine($"{potionStock} healing potions left.");
        Console.WriteLine($"Use a healing potion? Y/N");
        ConsoleKeyInfo keyPress = Console.ReadKey(true);
        char hitKey = Char.ToUpper(keyPress.KeyChar);

        switch (hitKey)
        {
            case 'Y':
                if (playerHealthCurrent >= playerHealthMax)
                {
                    Console.WriteLine($"Health already at maximum. Health potion will not be used.");
                    break;
                }
                else
                {
                    potionStock--;
                    Console.WriteLine($"Using a healing potion ({potionStock} remaining)...");
                    HealingPotion();
                    break;
                }
            case 'N':
                Console.WriteLine($"Healing potion not used.");
                break;
        }
    }
}

void HealingPotion()
{
    Console.WriteLine($"You use a healing potion.");
    int healedAmount = 0;
    for (int i = healingAmount; i > 0; i--)
    {
        playerHealthCurrent++;
        healedAmount++;
        if (playerHealthCurrent >= playerHealthMax)
        {
            playerHealthCurrent = playerHealthMax;
            Console.WriteLine($"Healed {healedAmount} HP. At maximum health ({playerHealthMax}).");
            break;
        }
    }
    Console.WriteLine($"Healed {healedAmount} HP. Current HP: {playerHealthCurrent} / {playerHealthMax}.");  
}


