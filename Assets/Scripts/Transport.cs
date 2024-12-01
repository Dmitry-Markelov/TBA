using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransportStatus
{
    Working,
    Critical,
    Damaged,
    NoFuel
}

public class Transport : MonoBehaviour
{
    private Rigidbody transportRigidBody;
    private HealthSystem healthSystem;
    private Engine engine;

    [SerializeField] public float acceleration;
    [SerializeField] public float maxSpeed;

    public bool isMoving { get; private set; } = false;
    public float baseAcceleration {get; private set; }  = 5f;
    public float baseMaxSpeed {get; private set; } = 10f;
    private float brakeForce = 5f;
    public TransportStatus CurrentState { get; private set; } = TransportStatus.Working;
    
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        engine = GetComponent<Engine>();
        transportRigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        acceleration = baseAcceleration;
        maxSpeed = baseMaxSpeed;
    }

    void Update()
    {
        UpdateTransportState();

        if (isMoving && CurrentState != TransportStatus.NoFuel)
        {
            Move();
        }
        else if (transportRigidBody.velocity.x != 0 && !isMoving)
        {
            Brake();
        }

        HandleDebugInput(); // переделать под InputSystem
    }

    private void UpdateTransportState()
    {
        float currentHealth = healthSystem.GetHealth();

        if (engine.fuel == 0)
        {
            CurrentState = TransportStatus.NoFuel;
            isMoving = false;
        }
        else if (currentHealth == 0)
        {
            CurrentState = TransportStatus.Critical;
            isMoving = false;
        }
        else if (currentHealth <= 60)
        {
            CurrentState = TransportStatus.Damaged;
        }
        else
        {
            CurrentState = TransportStatus.Working;
        }

        if (isMoving)
        {
            engine.ReduceFuel();
        }
    }

    private void Move()
    {
        if (transportRigidBody.velocity.x < maxSpeed)
        {
            Vector3 newForce = transform.right * acceleration;
            transportRigidBody.AddForce(newForce, ForceMode.Force);
        }

        if (transportRigidBody.velocity.x > maxSpeed)
        {
            Brake();
        }
    }

    private void Brake()
    {
        if (transportRigidBody.velocity.x > 0 )
        {
            Vector3 brakeForceVector = -transform.right * brakeForce;
            transportRigidBody.AddForce(brakeForceVector, ForceMode.Force);
        }
        else transportRigidBody.velocity = Vector3.zero;
    }

    public void ToggleMove()
    {
        isMoving = !isMoving;
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

    private void HandleDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.R)) // временный ремонт
        {
            Repair(10);
        }
        if (Input.GetKeyDown(KeyCode.T)) // временный дамаг
        {
            TakeDamage(10);
        }
    }
}
