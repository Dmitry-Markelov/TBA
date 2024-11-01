using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorld : MonoBehaviour
{
    public float moveSpeed = -0.0001f;
    Vector3 worldPosition = Vector3.zero;
    Transform world;
    // Start is called before the first frame update
    private void Awake()
    {
        world = GetComponent<Transform>();
    }

    void Start()
    {
        worldPosition = world.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        worldPosition.x += moveSpeed * Time.deltaTime;
        world.transform.position = worldPosition;
    }
}
