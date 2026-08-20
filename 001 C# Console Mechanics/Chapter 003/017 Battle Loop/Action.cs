public class Action
{
    // Fields
    private string name;
    private int cost;
    private int damage;
    private int healing;
    private float accuracyModifier;
    private bool isAvailable;
    private bool isSelected;

    // Properties
    public string Name {get; set;}
    public int Cost {get; set;}
    public int Damage {get; set;}
    public int Healing {get; set;}
    public float AccuracyModifier {get; set;}
    public bool IsAvailable {get; set;}
    public bool IsSelected {get; set;}

    public Action(string name, int cost, int damage, int healing, float accuracyModifier, bool isAvailable, bool isSelected)
    {
        this.Name = name;
        this.Cost = cost;
        this.Damage = damage;
        this.Healing = healing;
        this.AccuracyModifier = accuracyModifier;
        this.IsAvailable = isAvailable;
        this.IsSelected = isSelected;
    }
}