public struct Point
{
    public int x;
    public int y;
}

public class Program
{

    static void Main(string[] args)
    {
        Point P1;

        P1.x = 3;
        P1.y = 7;

        string text = "String";
        int number = 1;

        Console.WriteLine(typeof(string));
        Console.WriteLine(typeof(int));
        Console.WriteLine(typeof(Point));
    }


}