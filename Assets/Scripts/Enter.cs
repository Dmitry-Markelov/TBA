using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter : MonoBehaviour
{
    public Transform parent;
    private Transform playerTransform;
    private Transport transport;

    private GameObject player;
    public bool inTransport { get; private set; } = false;
    private bool isPlayerNearby = false;

    private float transportPosition = 5.75f;
    private float groundPosition = 3.9f; //переделать
    
    void Awake()
    {
        transport = FindObjectOfType<Transport>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }

    void Update()
    {
        HandleEnter();
    }

    private void HandleEnter()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!inTransport)
            {
                playerTransform.position = new Vector3(playerTransform.position.x, transportPosition, playerTransform.position.z);
                inTransport = true;
                player.transform.SetParent(transport.transform);
            }
            else if (!transport.isMoving)
            {
                playerTransform.position = new Vector3(playerTransform.position.x, groundPosition, playerTransform.position.z);
                inTransport = false;
                player.transform.SetParent(parent);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //переделать
        {
            isPlayerNearby = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) //переделать
        {
            isPlayerNearby = false;
        }
    }
}
