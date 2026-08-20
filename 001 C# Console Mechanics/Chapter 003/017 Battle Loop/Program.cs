using System;
using System.Collections.Generic;

public class Program
{
    Action AimedAttack = new Action("Aimed Attack", 30, 10, 0, 1f, true, false);
    Action QuickAttack = new Action("Quick Attack", 20, 10, 0, 0.7f, true, false);
    Action MedKit = new Action("Med Kit", 100, 0, 25, 1f, true, false);
    Action Flee = new Action("Flee", 50, 0, 0, 0.5f, true, false);

    public List<Action> availableActions = new List<Action>();

    public Action playerPendingAction = null;
    public Action enemyPendingAction = null;

    public int currentTime = 0;
    public int playerActionTurn;
    public int enemyActionTurn;

    public int playerHealthMax = 100;
    public int enemyHealthMax = 100;
    public int playerHealthCurrent;
    public int enemyHealthCurrent;

    public bool isPlayerTurn = true;
    public int selectionIndex = 0;

    public bool isPlayerAlive = true;
    public bool isEnemyAlive = true;

    public int maxTime = 240;

    void Wait(int duration)
    {
        System.Threading.Thread.Sleep(duration);
    }

    public static void Main(String[] args)
    {
        Program program = new Program();

        Console.Clear();
        program.Initialize();

        program.isPlayerTurn = program.IsPlayerTurn();

        if (program.isPlayerTurn)
        {
            program.PlayerTurn();
        }
        else
        {
            program.EnemyTurn();
        }

        while (program.isPlayerAlive && program.isEnemyAlive)
        {
            while (program.currentTime != program.playerActionTurn && program.currentTime != program.enemyActionTurn)
            {
                program.ProcessTurn();
            }

            if (program.currentTime == program.playerActionTurn)
            {
                Console.WriteLine($"Player action {program.playerPendingAction.Name} resolves.");
                program.PlayerTurn();
            }

            if (program.currentTime == program.enemyActionTurn)
            {
                Console.WriteLine($"Enemy action {program.enemyPendingAction.Name} resolves.");
                program.EnemyTurn();
            }

            if (program.currentTime == program.maxTime)
            {
                Console.WriteLine($"Time limit reached, terminating...");
                program.isPlayerAlive = false;
            }

        }
    }

    public bool IsPlayerTurn()
    {
        var random = new Random();
        int randomRoll = random.Next(0, 2);
        if (randomRoll == 0) return true;
        else return false;
    }

    public void ProcessTurn()
    {
        RefreshUI();

        if (currentTime == playerActionTurn)
        {
            isPlayerTurn = true;
            PlayerTurn();
        }

        if (currentTime == enemyActionTurn)
        {
            EnemyTurn();
        }

        Wait(100);
        currentTime++;
        RefreshUI();
    }

    public void Initialize()
    {
        availableActions.Clear();
        availableActions.Add(AimedAttack);
        availableActions.Add(QuickAttack);
        availableActions.Add(MedKit);
        availableActions.Add(Flee);

        playerHealthCurrent = playerHealthMax;
        enemyHealthCurrent = enemyHealthMax;
    }

    public void RefreshUI()
    {
        Console.Clear();
        Console.WriteLine($"Current time: " + currentTime);
        Console.WriteLine();
        Console.WriteLine($"Player action resolves at " + playerActionTurn);
        Console.WriteLine($"Enemy action resolves at " + enemyActionTurn);
        Console.WriteLine();
    }

    public void EnemyTurn()
    {
        var random = new Random();
        int randomSelection = random.Next(availableActions.Count);
        enemyPendingAction = availableActions[randomSelection];
        enemyActionTurn = currentTime + enemyPendingAction.Cost;
        ProcessTurn();
    }

    public void PlayerTurn()
    {
        foreach (var entry in availableActions)
        {
            entry.IsSelected = false;
        }

        selectionIndex = 0;
        availableActions[selectionIndex].IsSelected = true;

        while (isPlayerTurn)
        {
            PlayerTurnMenu();
        }

        ProcessTurn();
    }

    public void PlayerTurnMenu()
    {
        RefreshUI();

        Console.WriteLine($"W/S to select, F to confirm");

        availableActions[selectionIndex].IsSelected = true;

        foreach (var entry in availableActions)
        {
            if(entry.IsSelected)
            {
                Console.Write(">>> ");
            }
            else
            {
                Console.Write("    ");
            }

            Console.Write($"{entry.Name} - {entry.Cost} time units");

            if(entry.IsSelected)
            {
                Console.Write(" <<<");
            }
            else
            {
                Console.Write("    ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();

        ConsoleKeyInfo keyPress = Console.ReadKey(true);
        char key = Char.ToUpper(keyPress.KeyChar);

        ProcessInput(key);
    }

    public void ProcessInput(char key)
    {
        bool inputRequired = true;

        switch (key)
        {
            case 'W':
                if (selectionIndex <= 0)
                {
                    selectionIndex = availableActions.Count - 1;
                }
                else
                {
                    selectionIndex--;
                }

                foreach (var entry in availableActions)
                {
                    entry.IsSelected = false;
                }

                availableActions[selectionIndex].IsSelected = true;
                inputRequired = false;
                break;

            case 'S':
                if (selectionIndex >= availableActions.Count - 1)
                {
                    selectionIndex = 0;
                }
                else
                {
                    selectionIndex++;
                }

                foreach (var entry in availableActions)
                {
                    entry.IsSelected = false;
                }

                availableActions[selectionIndex].IsSelected = true;
                inputRequired = false;
                break;

            case 'F':
                playerPendingAction = availableActions[selectionIndex];
                playerActionTurn = currentTime + playerPendingAction.Cost;
                inputRequired = false;
                isPlayerTurn = false;
                break;
        }
    }
}