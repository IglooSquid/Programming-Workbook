public static class Program
{
    static int healingInterval = 3;
    static int spawnInterval = 5;
    static int maxTurns = 20;

    static int currentTurn = 0;

    public static void Main(String[] args)
    {
        while (currentTurn < maxTurns)
        {
            ProcessTurn();
        }

        EndBattle();
    }

    static void ProcessTurn()
    {
        currentTurn++;
        Refresh();
        if (currentTurn % healingInterval == 0)
        {
            Healing();
        }
        if (currentTurn % spawnInterval == 0)
        {
            Spawn();
        }
        Console.ReadKey();
    }

    static void Refresh()
    {
        Console.Clear();
        Console.WriteLine($"Current turn: {currentTurn}");
        Console.WriteLine();
    }

    static void Healing()
    {
        Console.WriteLine($"Healing...");
    }

    static void Spawn()
    {
        Console.WriteLine($"Spawning enemies...");
    }

    static void EndBattle()
    {
        Console.WriteLine($"Time's up!");
    }
}