using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    private Enter enter;
    private Inventory inventory;

    [NonSerialized] public float acceleration;
    [NonSerialized] public float maxSpeed;

    private bool upgradeReady = false;
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
        transport = FindAnyObjectByType<Transport>();
        enter = FindAnyObjectByType<Enter>();
        inventory = FindAnyObjectByType<Inventory>();
    }

    void Start()
    {
        acceleration = baseAcceleration;
        maxSpeed = speed.GetCurrentValue();

        fuel = fuelCapacity.GetCurrentValue();
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
        fuel = math.clamp(fuel, 0, fuelCapacity.GetCurrentValue());
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
        HandleUpdate(KeyCode.F, 1, 1, () => AddFuel(100));
        HandleUpdate(KeyCode.R, 2, 1, () => transport.Repair(50));
        HandleUpdate(KeyCode.Alpha1, 3, 1, () => speed.Upgrade());
        HandleUpdate(KeyCode.Alpha2, 3, 1, () => durability.Upgrade());
        HandleUpdate(KeyCode.Alpha3, 3, 1, () => fuelCapacity.Upgrade());
    }

    private void HandleUpdate(KeyCode Key, int itemId, int itemCount, Action nameAction)
    {
        if (Input.GetKeyDown(Key) && upgradeReady && enter.inTransport)
        {
            if (inventory.DeleteItemById(itemId, itemCount))
            {
                nameAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            upgradeReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            upgradeReady = false;
        }
    }
}
