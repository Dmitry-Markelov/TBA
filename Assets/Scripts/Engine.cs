using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    private Transport transport;
    private float baseSpeed = 5f;
    public float fuel;
    private float fuelRate = 0.1f;
    [SerializeField] public float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transport = GetComponent<Transport>();
        currentSpeed = baseSpeed;
        fuel = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transport.currentState == TransportStatus.Damaged)
        {
            FuelLeak();
        }
        switch (transport.currentState)
        {
            case TransportStatus.Damaged:
                currentSpeed = baseSpeed * 0.75f;
                break;
            case TransportStatus.Critical:
                currentSpeed = 0;
                break;
            default:
                currentSpeed = baseSpeed;
                break;
        }
        if (Input.GetKeyDown(KeyCode.F)) // временная заправка
        {
            AddFuel(10);
        }
    }

    public float GetSpeed()
    {
        return currentSpeed;
    }

    public void AddFuel(float value)
    {
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

}
