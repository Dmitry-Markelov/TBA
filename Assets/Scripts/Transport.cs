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
    private Rigidbody carRB;
    public TransportStatus currentState = TransportStatus.Working;
    private HealthSystem healthSystem;
    private Engine engine;

    public float baseSpeed = 5f;
    [SerializeField] public float currentSpeed;
    private float maxSpeed = 10f;

    public bool isMove = false;
    
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        engine = GetComponent<Engine>();
        carRB = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float currentHealth = healthSystem.GetHealth();

        if (engine.fuel == 0) {
            currentState = TransportStatus.NoFuel;
            isMove = false;
        } else if (currentHealth == 0) {
            currentState = TransportStatus.Critical;
        } else if (currentHealth <= 60) {
            currentState = TransportStatus.Damaged;
        } else {
            currentState = TransportStatus.Working;
        }

        if (isMove)
        {
            engine.ReduceFuel();
        }

        if (isMove && currentState != TransportStatus.NoFuel)
        {
            Move();
        }
    }

    private void Move()
    {
        float velocity = Vector3.Dot(transform.right, carRB.velocity);

        if (velocity < maxSpeed) 
        {
            Vector3 newPos = transform.right * currentSpeed;
            carRB.AddForce(newPos, (ForceMode)ForceMode2D.Force);
        }
    }

    public void ToggleMove()
    {
        isMove = !isMove;
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
