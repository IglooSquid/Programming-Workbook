int number = 10;

void DoubleValue(ref int refValue)
{
    refValue *= 2;
}

DoubleValue(ref number);
Console.WriteLine(number); // prints the updated value