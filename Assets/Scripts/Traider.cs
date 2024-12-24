using System.Collections.Generic;
using UnityEngine;

public class Traider : MonoBehaviour
{
    public ItemDatabase itemDatabase;
    private Inventory inventory;
    private TraiderUI traiderUI;

    public GameObject UI;

    public List<TraiderItem> availableItems = new List<TraiderItem>();
    public int maxItemsForSale = 4;
    public int traiderMoney = 5;

    private Dictionary<KeyCode, int> keyToItemId = new Dictionary<KeyCode, int>
    {
        { KeyCode.Alpha1, 1 },
        { KeyCode.Alpha2, 2 },
        { KeyCode.Alpha3, 3 },
        { KeyCode.Alpha4, 5 },
        { KeyCode.Alpha5, 6 }
    };

    System.Random rnd = new System.Random();

    private void Awake()
    {
        traiderUI = FindAnyObjectByType<TraiderUI>();
        inventory = FindAnyObjectByType<Inventory>();
    }

    private void Start()
    {
        UI.SetActive(false);
    }

    private void FixedUpdate()
    {
        HandleBuy();
    }

    public void GenerateItemsForSale()
    {
        availableItems.Clear();

        int itemCount = 6;
        for(int i = 1; i <= itemCount; i++)
        {
            if (i != 4)
            {
                int cost = rnd.Next(1, 5);
                int quanity = rnd.Next(1, 5);
                TraiderItem randomItem = new TraiderItem(i, cost, quanity);
                availableItems.Add(randomItem);
            }
        }

        traiderUI.UpdateTraderUI();
    }

    public void HandleBuy()
    {
        foreach(var keyPair in keyToItemId)
        {
            if (Input.GetKeyDown(keyPair.Key))
            {
                BuyItem(keyPair.Value, 1);
            }
        }
    }

    public bool BuyItem(int itemId, int quantity)
    {
        InventoryItem item = itemDatabase.GetItemById(itemId);
        if (item == null)
        {
            Debug.LogWarning($"Предмет с {itemId} не найден!");
            return false;
        }

        foreach (var traiderItem in availableItems)
        {
            if (traiderItem.id == itemId && traiderItem.count > 0)
            {
                int? playerMoney = inventory.GetQuanityItemById(4);
                if (playerMoney != null && playerMoney >= traiderItem.cost)
                {
                    inventory.AddItemById(itemId, quantity);
                    inventory.DeleteItemById(4, traiderItem.cost);
                    traiderMoney += traiderItem.cost;

                    DeleteTraderItem(itemId);
                    traiderUI.UpdateTraderUI();
                    return true;
                }
                else
                {
                    Debug.LogWarning("Недостаточно средств для покупки!");
                }
            }
        }
        
        return false;
    }

    public void DeleteTraderItem(int id)
    {
        foreach (TraiderItem traiderItem in availableItems)
        {
            if (traiderItem.id == id)
            {
                traiderItem.count--;
            }
        }

        traiderUI.UpdateTraderUI();
    }

    public void ShowUI()
    {
        UI.SetActive(true);
    }

    public void HideUI()
    {
        UI.SetActive(false);
    }
}
