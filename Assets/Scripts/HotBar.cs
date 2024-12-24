using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    private Inventory inventory;

    public Text fuelText;
    public Text repairText;
    public Text upgradeText;
    public Text diamondsText;
    public Text sawText;
    public Text pickAxeText;

    private void Awake()
    {
        inventory = FindAnyObjectByType<Inventory>();
    }

    private void Start()
    {
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        List<(InventoryItem item, int quantity)> items = inventory.GetInventoryItems();

        foreach (var (item, quantity) in items)
        {
            switch (item.id)
            {
                case 1:
                    fuelText.text = $"{quantity}/{item.maxStack}";
                    break;
                case 2:
                    repairText.text = $"{quantity}/{item.maxStack}";
                    break;
                case 3:
                    upgradeText.text = $"{quantity}/{item.maxStack}";
                    break;
                case 4:
                    diamondsText.text = quantity.ToString();
                    break;
                case 5:
                    sawText.text = quantity.ToString();
                    break;
                case 6:
                    pickAxeText.text = quantity.ToString();
                    break;
            }
        }
    }
}