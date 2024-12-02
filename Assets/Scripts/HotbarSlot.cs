using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    public Text itemCount; // Текст для отображения количества

    public void UpdateSlot(int count)
    {
        if (count > 0)
        {
            itemCount.text = count.ToString(); // Отображаем количество
        }
        else
        {
            itemCount.text = ""; // Если количество 0, очищаем текст
        }
    }
}
