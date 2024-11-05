using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    private Transport transport;
    private Enter enter;
    private bool isPlayerNearby = false;

    // Start is called before the first frame update
    void Awake()
    {
        transport = FindObjectOfType<Transport>();
        enter = FindObjectOfType<Enter>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && enter.inTransport)
        {
            transport.ToggleMove();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Collision detected");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
