using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public enum TransportStatus
{
    Working,
    Critical,
    Damaged
}

public class Transport : MonoBehaviour
{
    public TransportStatus currentState = TransportStatus.Working;
    private HealthSystem healthSystem;
    private Engine engine;

    public bool isMove = false;
    
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        engine = GetComponent<Engine>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentHealth = healthSystem.GetHealth();

        if (currentHealth == 0) {
            currentState = TransportStatus.Critical;
        } else if (currentHealth <= 60) {
            currentState = TransportStatus.Damaged;
        } else {
            currentState = TransportStatus.Working;
        }
    }

    void ChangeMove()
    {
        isMove = !isMove;
    }

    public void TakeDamage(float damage)
    {
        healthSystem.TakeDamage(damage);
    }

    public void Repair(float value)
    {
        healthSystem.Rapair(value);
    }

    public void ToggleEngine()
    {

    }
}
