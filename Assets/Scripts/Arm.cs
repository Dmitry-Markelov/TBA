using UnityEngine;

public class Arm : MonoBehaviour
{
    private Transport transport;
    private Enter enter;
    
    private bool isPlayerNearby = false;

    void Awake()
    {
        transport = FindObjectOfType<Transport>();
        enter = FindObjectOfType<Enter>();
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && enter.inTransport)
        {
            if (!transport.inObstacle)
            {
                transport.ToggleMove();
            }
        }
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
