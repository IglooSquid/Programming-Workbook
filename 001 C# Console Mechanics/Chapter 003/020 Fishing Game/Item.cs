using FishingGame;
using System;
using System.Collections.Generic;

public class Item
{
    public enum ItemCategory
    {
        Fish,
        Treasure,
        Trash
    }

    private string name;
    private int value;
    private int baseCatchChance;
    private ItemCategory category;

    public string Name {get; set;}
    public int Value {get; set;}
    public int BaseCatchChance {get; set;}
    public ItemCategory Category {get; set;}

    public Item (string name, int value, int baseCatchChance, ItemCategory category)
    {
        this.Name = name;
        this.Value = value;
        this.BaseCatchChance = baseCatchChance;
        this.Category = category;
    }
}