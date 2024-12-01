using UnityEngine;

public class LootObject : MonoBehaviour
{
    public enum LootType { Cave, AbandonedHouse, Trader }
    public LootType lootType;

    public void Interact()
    {
        if (lootType == LootType.Cave || lootType == LootType.AbandonedHouse)
        {
            LootTable lootTable = GetComponent<LootTable>();
            if (lootTable != null)
            {
                string loot = lootTable.GetRandomLoot();
                Debug.Log("Вы получили: " + loot);
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