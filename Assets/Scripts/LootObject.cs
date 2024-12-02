using UnityEngine;

public class LootObject : MonoBehaviour
{
    private Inventory inventory;

    public enum LootType { Cave, AbandonedHouse, Trader }
    public LootType lootType;

    private void Awake()
    {
        inventory = FindAnyObjectByType<Inventory>();
    }

    public void Interact()
    {
        if (lootType == LootType.Cave || lootType == LootType.AbandonedHouse)
        {
            LootTable lootTable = GetComponent<LootTable>();
            if (lootTable != null)
            {
                LootItem loot = lootTable.GetRandomLoot();

                bool isAdded = inventory.AddItemById(loot.id, 1);
                if (isAdded)
                {
                    Debug.Log("Вы получили: " + loot.itemName);
                }
                else
                {
                    Debug.Log("Не удалось добавить предмет.");
                }
            }
            else
            {
                Debug.LogWarning("LootTable не найден на объекте!");
            }
        }
        else if (lootType == LootType.Trader)
        {
            //логика торговли
        }

        Destroy(gameObject);
    }
}