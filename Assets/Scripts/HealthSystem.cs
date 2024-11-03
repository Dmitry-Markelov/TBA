using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < maxHealth) Critical();
    }

    public void Rapair(float value)
    {
        currentHealth += value;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
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
