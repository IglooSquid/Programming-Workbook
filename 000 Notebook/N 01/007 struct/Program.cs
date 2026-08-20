public struct Point
{
    public int x;
    public int y;
}

public class Program
{
    public static void Main(string[] args)
    {
        Point P1;

        P1.x = 3;
        P1.y = 7;

        Console.WriteLine(P1.x + " " + P1.y);
    }    
}
