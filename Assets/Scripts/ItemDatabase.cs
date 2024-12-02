using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<InventoryItem> items = new List<InventoryItem>();

    public InventoryItem GetItemById(int id)
    {
        return items.Find(item => item.id == id);
    }
}