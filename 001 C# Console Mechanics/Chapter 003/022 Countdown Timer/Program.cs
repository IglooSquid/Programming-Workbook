using System;
using System.Collections.Generic;

public static class Program
{
    static int startingTime = 10;
    static int currentTime;

    static int pauseTime;
    static string currentMessage = null;
    static int maxTime = 20;

    public static void Main(String[] args)
    {
        startingTime = GetStartingTime();
        Refresh();
        pauseTime = GetPauseTime(startingTime);
        currentTime = startingTime;
        Refresh();
        while (currentTime > 0)
        {
            Refresh();
            Countdown();
        }
        End();
    }

    public static void Wait(int duration)
    {
        System.Threading.Thread.Sleep(duration);
    }

    public static void Countdown()
    {
            currentTime--;
            Console.Beep();
            if (currentTime == pauseTime)
            {
                Pause();
            }            
            else
            {
                Wait(1000);
            }


            Refresh();
            currentMessage = null;
    }

    public static int GetPauseTime(int maxTime)
    {
        int value = (int)maxTime / 2;
        return value;
    }

    public static void End()
    {
        Refresh();
        Console.WriteLine("Boom.");
    }

    public static int GetStartingTime()
    {
        bool entryRequired = true;

        while (entryRequired)
        {
            Refresh();
            Console.WriteLine($"Enter timer starting value.");
            string entry = Console.ReadLine();
            int value;
            if (int.TryParse(entry, out value))
            {
                if (value > maxTime || value <= 0)
                {
                    Console.WriteLine($"Select a number between 1 and {maxTime}.");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine($"Starting time set: {value}");
                    entryRequired = false;
                    Console.ReadKey(true);
                    return value;
                }
            }
            else
            {
                Console.WriteLine($"Enter a valid number.");
                Console.ReadKey(true);
            }
        }

        return startingTime;
    }

    public static void Pause()
    {
        currentMessage = "PAUSED - Press any key to continue";
        Refresh();
        Console.ReadKey(true);
    }

    public static void Refresh()
    {
        Console.Clear();
        Console.WriteLine(currentTime);
        Console.WriteLine(currentMessage);
    }
}