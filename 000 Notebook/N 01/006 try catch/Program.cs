Console.WriteLine("Enter an integer.");

try
{
    string userInput = Console.ReadLine();
    int entry = int.Parse(userInput);
    Console.WriteLine($"{entry} entered - valid number.");
}
catch
{
    Console.WriteLine("Invalid number.");
}