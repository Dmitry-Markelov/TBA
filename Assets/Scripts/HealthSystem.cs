using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth { get; private set; } = 100f;
    public float currentHealth { get; private set; }

    void Start()
    {
        currentHealth = maxHealth;    
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            Debug.LogWarning("Wrong damage!");
            return;
        }

        currentHealth -= damage;
        currentHealth = math.clamp(currentHealth, 0, maxHealth);
    }

    public void Rapair(float value)
    {
        if (value < 0)
        {
            Debug.LogWarning("Wrong repair value!");
            return;
        }
        
        currentHealth += value;
        currentHealth = math.clamp(currentHealth, 0, maxHealth);
    }

    private void Critical()
    {
        Debug.Log($"Транспорт в критическом состоянии!");
        // остановка всех систем, визуальные эффекты
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}
