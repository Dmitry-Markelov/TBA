using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public MoveWorld moveWorld;
    private bool isPlayerNearby = false;

    // Start is called before the first frame update
    void Awake()
    {
        moveWorld = FindObjectOfType<MoveWorld>();
    }
    void Start()
    {
        bool isMove = moveWorld.isMove;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            moveWorld.ChangeMove();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arm") || other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Arm") || other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
