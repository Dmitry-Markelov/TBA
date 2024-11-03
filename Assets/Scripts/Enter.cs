using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter : MonoBehaviour
{
    private Transform playerPos;
    public MoveWorld moveWorld;
    bool isTransport = false;
    private bool isPlayerNearby = false;
    float transpPos = 5.75f;
    float groundPos = 3.9f; //переделать
    
    void Awake()
    {
        moveWorld = FindObjectOfType<MoveWorld>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!isTransport)
            {
                playerPos.position = new Vector3(playerPos.position.x, transpPos, playerPos.position.z);
                isTransport = true;
            } else if (!moveWorld.isMove) {
                playerPos.position = new Vector3(playerPos.position.x, groundPos, playerPos.position.z);
                isTransport = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("Entrance")) //переделать
        {
            isPlayerNearby = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Entrance")) //переделать
        {
            isPlayerNearby = false;
        }
    }
}
