using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootItem
{
    public string itemName;
    public int chance;
}

public class LootTable : MonoBehaviour
{
    public List<LootItem> lootItems = new List<LootItem>();

    public string GetRandomLoot()
    {
        int totalChance = 0;

        foreach (var item in lootItems)
        {
            totalChance += item.chance;
        }

        int randomValue = Random.Range(0, totalChance);
        int currentChance = 0;

        foreach (var item in lootItems)
        {
            currentChance += item.chance;
            if (randomValue < currentChance)
            {
                return item.itemName;
            }
        }

        return null;
    }
}