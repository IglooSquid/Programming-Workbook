public class Program
{
    public const int hoursInDay = 24;

    public int currentDay = 0;
    public int currentHour = 0;
    public int currentRations = 0;
    public int startingRations = 5;

    public int restDuration = 8;
    public int healingAmount = 30;
    public int healingVariation = 5;

    public int maxHealth = 100;
    public int currentHealth;

    public char restKey = 'R';
    public char departKey = 'X';

    public bool isActionRequired = true;
    public bool isCamped = true;

    // Main ========================================================================================
    public static void Main(String[] args)
    {
        Program program = new Program();

        program.Initialize();
        program.Refresh();

        while (program.isCamped)
        {
            program.PlayerAction();
        }
    }

    // Methods =====================================================================================
    public void Initialize()
    {
        currentRations = startingRations;
        currentDay = 1;
        currentHour = RandomizeTime(hoursInDay);
        currentHealth = RandomizeHealth(maxHealth);
    }

    public void Wait(int duration)
    {
        System.Threading.Thread.Sleep(duration);
    }

    public int RandomizeHealth(int max)
    {
        var random = new Random();
        int randomHealth = random.Next(max);
        return randomHealth;
    }

    public int RandomizeTime(int time)
    {
        var random = new Random();
        int randomTime = random.Next(time);
        return randomTime;
    }

    public void Refresh()
    {
        Console.Clear();
        Console.WriteLine($"========================");
        Console.WriteLine($"=== DAY {currentDay} === HOUR {currentHour} ===");
        Console.WriteLine($"Current rations remaining: {currentRations}");
        Console.WriteLine($"Health: {currentHealth} / {maxHealth}");
        Console.WriteLine($"========================");
        Console.WriteLine();
        if (isActionRequired)
        {
            Console.WriteLine($"Press {restKey} to rest. Press {departKey} to depart.");
            Console.WriteLine();
        }
    }

    public void PlayerAction()
    {
        bool isInputRequired = true;
        while (isInputRequired)
        {
            ConsoleKeyInfo keyPress = Console.ReadKey(true);
            char key = Char.ToUpper(keyPress.KeyChar);

            switch (key)
            {
                case 'R':
                    isInputRequired = false;
                    Rest();
                    break;
                case 'X':
                    isInputRequired = false;
                    Depart();
                    break;
            }
        }
    }

    public void Rest()
    {
        if (currentRations > 0)
        {
            isActionRequired = false;
            currentRations--;

            var random = new Random();
            int healing = healingAmount + (random.Next(-healingVariation, healingVariation));
            int healingPerHour = Convert.ToInt32(healing / restDuration);
            int amountRecovered = 0;

            for (int i = 0; i < restDuration; i++)
            {
                Refresh();
                currentHour++;
                if (currentHour >= hoursInDay)
                {
                    currentHour -= hoursInDay;
                    currentDay++;
                }

                for (int j = 0; j < healingPerHour; j++)
                {
                    if (currentHealth < maxHealth)
                    {
                        currentHealth++;
                        amountRecovered++;
                    }
                }

                if (currentHealth >= maxHealth)
                {
                    currentHealth = maxHealth;
                }

                Wait(500);
                Refresh();
            }

            isActionRequired = true;
            Refresh();
            Console.WriteLine($"Consumed a ration, and recovered {amountRecovered} health.");
            Console.WriteLine();
        }

        else
        {
            Console.WriteLine($"You have no more rations left. Resting aborted.");
        }
    }

    public void Depart()
    {
        Console.WriteLine($"Be seeing you.");
        isCamped = false;
    }
}