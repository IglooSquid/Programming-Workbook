void Countdown(int number)
{
    Console.WriteLine(number);

    if (number > 1)
    {
        number--;
        Countdown(number);
    }
}

Countdown(5);