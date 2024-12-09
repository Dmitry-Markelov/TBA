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

    public bool isMoving { get; private set; } = false;

    public TransportStatus CurrentState { get; private set; } = TransportStatus.Working;
    
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        engine = FindAnyObjectByType<Engine>();
        transportRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
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
        if (transportRigidBody.velocity.x < engine.speed.GetCurrentValue())
        {
            Vector3 newForce = transform.right * engine.acceleration;
            transportRigidBody.AddForce(newForce, ForceMode.Force);
        }

        if (transportRigidBody.velocity.x > engine.speed.GetCurrentValue())
        {
            Brake();
        }
    }

    private void Brake()
    {
        if (transportRigidBody.velocity.x > 0 )
        {
            Vector3 brakeForceVector = -transform.right * engine.brakeForce;
            transportRigidBody.AddForce(brakeForceVector, ForceMode.Force);
        }
        else transportRigidBody.velocity = Vector3.zero;
    }

    public void ToggleMove()
    {
        isMoving = !isMoving;
    }

    public void GetDamage(float damage)
    {
        healthSystem.GetDamage(damage);
    }

    public void Repair(float value)
    {
        healthSystem.Repair(value);
    }

    public void ToggleEngine()
    {

    }

    private void HandleDebugInput()
    {
        //if (Input.GetKeyDown(KeyCode.R)) // временный ремонт
        //{
        //    Repair(10);
        //}
        if (Input.GetKeyDown(KeyCode.T)) // временный дамаг
        {
            GetDamage(10);
        }
    }
}
