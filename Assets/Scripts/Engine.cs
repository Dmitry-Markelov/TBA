using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    private Transport transport;
    public float fuel;
    private float fuelRate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        transport = GetComponent<Transport>();
        fuel = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transport.currentState == TransportStatus.Damaged || transport.currentState == TransportStatus.Critical)
        {
            FuelLeak();
        }
        switch (transport.currentState)
        {
            case TransportStatus.Damaged:
                transport.currentSpeed = transport.baseSpeed * 0.5f;
                break;
            case TransportStatus.Critical:
                transport.currentSpeed = 0;
                break;
            default:
                transport.currentSpeed = transport.baseSpeed;
                break;
        }
        if (Input.GetKeyDown(KeyCode.F)) // временная заправка
        {
            AddFuel(10);
        }
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
