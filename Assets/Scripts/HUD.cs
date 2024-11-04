using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private Transport transport;
    private HealthSystem healthSystem;
    private Engine engine;

    public Text HP;
    public Text State;
    public Text Fuel;

    void Awake()
    {
        transport = FindObjectOfType<Transport>();
        healthSystem = FindObjectOfType<HealthSystem>();
        engine = FindObjectOfType<Engine>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HP.text = "HP: " + healthSystem.currentHealth;
        State.text = "State: " + transport.currentState;
        Fuel.text = "Fuel: " + engine.fuel;
    }
}
