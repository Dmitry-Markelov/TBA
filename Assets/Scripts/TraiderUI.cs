using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraiderUI : MonoBehaviour
{
    public Text slot1;
    public Text slot2;
    public Text slot3;

    public Text slot5;
    public Text slot6;

    private Traider trader;

    private void Awake()
    {
        trader = FindAnyObjectByType<Traider>();
    }

    private void FixedUpdate()
    {
        UpdateTraderUI();
    }

    public void UpdateTraderUI()
    {
        Text[] slots = new Text[] { slot1, slot2, slot3, null, slot5, slot6 };

        foreach (TraiderItem item in trader.availableItems)
        {
            if (item.id > 0 && item.id <= slots.Length && slots[item.id - 1] != null)
            {
                slots[item.id - 1].text = $"Cost: {item.cost}";
            }
        }
    }
}
