public class Monster
{
    public string name;
    public int attackDamageMin;
    public int attackDamageMax;
    public int criticalChance;

    public Monster (string name, int attackDamageMin, int attackDamageMax, int criticalChance)
    {
        this.name = name;
        this.attackDamageMin = attackDamageMin;
        this.attackDamageMax = attackDamageMax;
        this.criticalChance = criticalChance;
    }
}