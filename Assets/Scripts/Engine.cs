using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeableStat
{
    public string name;
    public float baseValue;
    public int level = 1;
    public float multiplier = 0.3f;

    public float GetCurrentValue()
    {
        return baseValue * (1 + multiplier * (level - 1));
    }

    public void Upgrade()
    {
        level++;
    }
}

public class Engine : MonoBehaviour
{
    private Transport transport;

    [NonSerialized] public float acceleration;
    [NonSerialized] public float maxSpeed;

    public float baseAcceleration { get; private set; } = 5f;
    public float baseMaxSpeed { get; private set; } = 10f;
    private float fuelRate = 0.1f;
    [SerializeField] public float brakeForce = 5f;
    [SerializeField] public float fuel;

    [SerializeField] public UpgradeableStat speed = new UpgradeableStat { name = "Скорость", baseValue = 10f };
    [SerializeField] public UpgradeableStat durability = new UpgradeableStat { name = "Прочность", baseValue = 100f };
    [SerializeField] public UpgradeableStat fuelCapacity = new UpgradeableStat { name = "Размер бака", baseValue = 100f };


    void Awake()
    {
        transport = GetComponent<Transport>();
    }

    void Start()
    {
        acceleration = baseAcceleration;

        fuel = 100f;
    }

    void FixedUpdate()
    {
        PrintStats();

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
                acceleration = baseAcceleration * 0.5f;
                maxSpeed = speed.GetCurrentValue() * 0.5f;
                break;
            case TransportStatus.Critical:
                acceleration = 0;
                break;
            default:
                maxSpeed = speed.GetCurrentValue();
                acceleration = baseAcceleration;
                break;
        }
    }

    public void UpgradeStat(string statName)
    {
        switch (statName)
        {
            case "speed":
                speed.Upgrade();
                break;
            case "durability":
                durability.Upgrade();
                break;
            case "fuelCapacity":
                fuelCapacity.Upgrade();
                break;
            default :
                Debug.LogWarning("Неизвестная характеристика: " + statName);
                break;
        }
    }

    public void PrintStats()
    {
        Debug.Log($"Скорость: {speed.GetCurrentValue()}, Уровень: {speed.level}");
        Debug.Log($"Прочность: {durability.GetCurrentValue()}, Уровень: {durability.level}");
        Debug.Log($"Объем бака: {fuelCapacity.GetCurrentValue()}, Уровень: {fuelCapacity.level}");
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
    }

    public void FuelLeak()
    {
        if (fuel > 0)
        {
            fuel -= 0.01f; // уменьшение топлива (типо вытекает при повреждении)
        }
        else fuel = 0;
    }

    private void HandleDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.F)) // временная заправка
        {
            AddFuel(10);
        }
    }
}
