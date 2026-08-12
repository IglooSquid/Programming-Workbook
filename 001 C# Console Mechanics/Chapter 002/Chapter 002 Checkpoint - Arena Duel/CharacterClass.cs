public class CharacterClass
{
    public string name;
    public int health;
    public int strength;
    public int agility;
    public int perception;

    public CharacterClass(string name, int health, int strength, int agility, int perception)
    {
        this.name = name;
        this.health = health;
        this.strength = strength;
        this.agility = agility;
        this.perception = perception;
    }
}