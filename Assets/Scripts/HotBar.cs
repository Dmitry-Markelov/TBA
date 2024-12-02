using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Inventory;

public class Hotbar : MonoBehaviour
{
    private Inventory inventory;

    public Text fuelText;
    public Text repairText;
    public Text upgradeText;
    public Text diamondsText;

    private void Awake()
    {
        inventory = FindAnyObjectByType<Inventory>();
    }

    public void UpdateInventory()
    {
        List<(InventoryItem item, int quantity)> items = inventory.GetInventoryItems();

        foreach (var (item, quantity) in items)
        {
            switch (item.id)
            {
                case 1:
                    fuelText.text = quantity.ToString() + "/5";
                    break;
                case 2:
                    repairText.text = quantity.ToString() + "/5";
                    break;
                case 3:
                    upgradeText.text = quantity.ToString() + "/2";
                    break;
                case 4:
                    diamondsText.text = quantity.ToString() + "/3";
                    break;
            }
        }
    }
}