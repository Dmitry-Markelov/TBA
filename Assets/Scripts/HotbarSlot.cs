using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    public Text itemCount;

    public void UpdateSlot(int count)
    {
        if (count > 0)
        {
            itemCount.text = count.ToString();
        }
        else
        {
            itemCount.text = "";
        }
    }
}
