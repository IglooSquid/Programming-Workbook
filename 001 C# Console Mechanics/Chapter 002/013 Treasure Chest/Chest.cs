public class Chest
{
    public string name;
    public List<Item> contentsList;
    public bool isOpened = false;

    public Chest(string name, List<Item> contentsList)
    {
        this.name = name;
        this.contentsList = contentsList;
    }
}