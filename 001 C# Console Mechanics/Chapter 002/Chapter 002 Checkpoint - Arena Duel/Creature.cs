public class Creature
{
    public string name;
    public int health;
    public int strength;
    public int agility;
    public int perception;
    public bool isPlayable;
    public string description;
    public int experienceValue;
    
    public Creature(string name, int health, int strength, int agility, int perception, bool isPlayable, string description)
    {
        this.name = name;
        this.health = health;
        this.strength = strength;
        this.agility = agility;
        this.perception = perception;
        this.isPlayable = isPlayable;
        this.description = description;
    }
}