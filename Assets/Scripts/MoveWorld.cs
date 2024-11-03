using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorld : MonoBehaviour
{
    private Engine engine;
    public float moveSpeed;
    public bool isMove = false;
    Vector3 worldPosition = Vector3.zero;
    Transform world;
    
    // Start is called before the first frame update
    private void Awake()
    {
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
        if (isMove)
        {
            worldPosition.x -= moveSpeed * Time.deltaTime;
            world.transform.position = worldPosition;
        }
    }

    public void ToggleMove()
    {
        isMove = !isMove;
    }
    
}
