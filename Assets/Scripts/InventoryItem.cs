using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public int id;
    public string name;
    public int maxStack;

    public InventoryItem(int id, string name, int maxStack)
    {
        this.id = id;
        this.name = name;
        this.maxStack = maxStack;
    }
}