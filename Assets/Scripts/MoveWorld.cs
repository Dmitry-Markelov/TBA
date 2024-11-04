using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorld : MonoBehaviour
{
    private Transport transport;
    private Engine engine;
    public float moveSpeed;
    Vector3 worldPosition = Vector3.zero;
    Transform world;
    
    // Start is called before the first frame update
    private void Awake()
    {
        transport = GameObject.FindWithTag("Entrance").GetComponent<Transport>();
        engine = GameObject.FindWithTag("Transport").GetComponentInChildren<Engine>();
        world = GetComponent<Transform>();
    }

    void Start()
    {
        worldPosition = world.transform.position;
    
        moveSpeed = engine.GetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (transport.isMove && transport.currentState != TransportStatus.NoFuel)
        {
            Move();
        }
    }

    private void Move()
    {
        worldPosition.x -= moveSpeed * Time.deltaTime;
        world.transform.position = worldPosition;
    }
    
}
