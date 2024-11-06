using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public enum TransportStatus
{
    Working,
    Critical,
    Damaged,
    NoFuel // Нужно учитывать, что топливо могло кончиться при поврежденном или критическом состоянии. И при заправке вернуть состояние. Или вообще убрать из состояния NoFuel
}

public class Transport : MonoBehaviour
{
    private Rigidbody carRigidBody;
    private HealthSystem healthSystem;
    private Engine engine;

    [SerializeField] public float baseAcceleration = 5f;
    [SerializeField] public float baseMaxSpeed = 10f;
    [SerializeField] public float acceleration;
    [SerializeField] public float maxSpeed;

    public bool isMoving { get; private set; } = false;
    public TransportStatus CurrentState = TransportStatus.Working;
    private float brakeForce = 5f;
    
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        engine = GetComponent<Engine>();
        carRigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        acceleration = baseAcceleration;
        maxSpeed = baseMaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float currentHealth = healthSystem.GetHealth();

        if (engine.fuel == 0) {
            CurrentState = TransportStatus.NoFuel;
            isMoving = false;
        } else if (currentHealth == 0) {
            CurrentState = TransportStatus.Critical;
            isMoving = false;
        } else if (currentHealth <= 60) {
            CurrentState = TransportStatus.Damaged;
        } else {
            CurrentState = TransportStatus.Working;
        }

        if (isMoving)
        {
            engine.ReduceFuel();
        }

        if (isMoving && CurrentState != TransportStatus.NoFuel)
        {
            Move();
        }
        else if (carRigidBody.velocity.x != 0 && !isMoving)
        {
            Brake();
        }

        if (Input.GetKeyDown(KeyCode.R)) // временный ремонт
        {
            Repair(10);
        }
        if (Input.GetKeyDown(KeyCode.T)) // временный дамаг
        {
            TakeDamage(10);
        }
    }

    private void Move()
    {
        if (carRigidBody.velocity.x < maxSpeed)
        {
            Vector3 newPos = transform.right * acceleration;
            carRigidBody.AddForce(newPos, (ForceMode)ForceMode2D.Force);
        }
        if (carRigidBody.velocity.x > maxSpeed)
        {
            Brake();
        }
    }

    private void Brake()
    {
        if (carRigidBody.velocity.x > 0 )
        {
            Vector3 newPos = -transform.right * brakeForce;
            carRigidBody.AddForce(newPos, (ForceMode)ForceMode2D.Force);
        }
        else carRigidBody.velocity = Vector3.zero;
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
}
