int number = 10;

void LabMethod (ref int num)
{
    num = 50;
}

LabMethod (ref number);
Console.WriteLine(number);