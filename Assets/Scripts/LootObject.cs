using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootObject : MonoBehaviour
{
    private Inventory inventory;
    private Transport transport;
    private Traider traider;

    System.Random rnd = new System.Random();

    public enum LootType { 
        Cave, AbandonedHouse,
        Traider,
        FallenTree, Rock,
        Storm
    }
    public LootType lootType;

    private void Awake()
    {
        inventory = FindAnyObjectByType<Inventory>();
        transport = FindAnyObjectByType<Transport>();
        traider = FindAnyObjectByType<Traider>();
    }

    public void Interact()
    {
        if (lootType == LootType.Cave || lootType == LootType.AbandonedHouse)
        {
            LootTable lootTable = GetComponent<LootTable>();
            if (lootTable != null)
            {
                LootItem loot = lootTable.GetRandomLoot();

                int count = rnd.Next(1, 3);
                bool isAdded = inventory.AddItemById(loot.id, count);
                if (isAdded)
                {
                    Debug.Log("Вы получили: " + loot.itemName);
                    Destroy(gameObject);
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
        else if (lootType == LootType.FallenTree || lootType == LootType.Rock)
        {
            int id = (lootType == LootType.FallenTree) ? 5 : 6;
                
            InventoryItem item = inventory.GetItemByID(id);
            if (item != null)
            {
                if(inventory.DeleteItemById(id, 1))
                {
                    transport.inObstacle = false;
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.Log("Не удалось найти предмет!");
            }
            return;
        }
        else if (lootType == LootType.Traider)
        {
            traider.GenerateItemsForSale();
            traider.ShowUI();

            StartCoroutine(WaitForEscape());
        }
    }

    private IEnumerator WaitForEscape()
    {
        while (!Input.GetKeyDown(KeyCode.Escape))
        {
            yield return null;
        }

        traider.HideUI();
        Destroy(gameObject);
    }
}