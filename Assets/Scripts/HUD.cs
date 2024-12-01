using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;

public class HUD : MonoBehaviour
{
    private Transport transport;
    private HealthSystem healthSystem;
    private Engine engine;
    private Player player;

    public Text pHpText;
    public Text tHpText;
    public Text stateText;
    public Text fuelText;

    void Awake()
    {
        transport = FindObjectOfType<Transport>();
        healthSystem = FindObjectOfType<HealthSystem>();
        engine = FindObjectOfType<Engine>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        pHpText.text = "HP: " + math.floor(player.currentHealth);
        tHpText.text = "HP: " + math.floor(healthSystem.currentHealth);
        stateText.text = "State: " + transport.CurrentState;
        fuelText.text = "Fuel: " + math.floor(engine.fuel);
    }
}
