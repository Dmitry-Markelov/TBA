using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    private Transport transport;
    private float baseSpeed = 5f;
    [SerializeField] public float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transport = GetComponent<Transport>();
        currentSpeed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public float GetSpeed()
    {
        return currentSpeed;
    }
}
