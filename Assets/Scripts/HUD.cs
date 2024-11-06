using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private Transport transport;
    private HealthSystem healthSystem;
    private Engine engine;

    public Text hpText;
    public Text stateText;
    public Text fuelText;

    void Awake()
    {
        transport = FindObjectOfType<Transport>();
        healthSystem = FindObjectOfType<HealthSystem>();
        engine = FindObjectOfType<Engine>();
    }

    void Update()
    {
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        hpText.text = "HP: " + healthSystem.currentHealth;
        stateText.text = "State: " + transport.CurrentState;
        fuelText.text = "Fuel: " + Mathf.Floor(engine.fuel);
    }
}
