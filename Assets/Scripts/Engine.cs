using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    private Transport transport;

    [SerializeField] public float fuel;
    private float fuelRate = 0.1f;

    void Awake()
    {
        transport = GetComponent<Transport>();
    }

    void Start()
    {
        fuel = 100f;
    }

    void Update()
    {
        HandleTransportStatus();

        if (transport.CurrentState == TransportStatus.Damaged || transport.CurrentState == TransportStatus.Critical)
        {
            FuelLeak();
        }
        
        HandleDebugInput(); // переделать под InputSystem
    }

    private void HandleTransportStatus()
    {
        switch (transport.CurrentState)
        {
            case TransportStatus.Damaged:
                transport.acceleration = transport.baseAcceleration * 0.5f;
                transport.maxSpeed = transport.baseMaxSpeed * 0.5f;
                break;
            case TransportStatus.Critical:
                transport.acceleration = 0;
                break;
            default:
                transport.maxSpeed = transport.baseMaxSpeed;
                transport.acceleration = transport.baseAcceleration;
                break;
        }
    }
    
    public void AddFuel(float value)
    {
        if (value < 0)
        {
            Debug.LogWarning("Wrong fuel value!");
            return;
        }

        fuel += value;
    }

    public void ReduceFuel()
    {
        if (fuel > 0)
        {
            fuel -= fuelRate;
        }
        else fuel = 0;

        Debug.Log(fuel);
    }

    public void FuelLeak()
    {
        if (fuel > 0)
        {
            fuel -= 0.01f; // уменьшение топлива (типо вытекает при повреждении)
        }
        else fuel = 0;

        Debug.Log(fuel);
    }

    private void HandleDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.F)) // временная заправка
        {
            AddFuel(10);
        }
    }
}
