using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Enter enter;
    private Player player;
    private LootObject currentLootObject;

    private void Awake()
    {
        enter = FindAnyObjectByType<Enter>();
        player = FindAnyObjectByType<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentLootObject != null && !enter.inTransport)
        {
            currentLootObject.Interact();
            currentLootObject = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Loot") || other.CompareTag("Obstacle") || other.CompareTag("Traider")) && !other.CompareTag("Transport"))
        {
            currentLootObject = other.GetComponent<LootObject>();
        }

        if (other.CompareTag("Storm"))
        {
            player.inStorm = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Loot") || other.CompareTag("Obstacle") || other.CompareTag("Traider")) && !other.CompareTag("Transport"))
        {
            currentLootObject = null;
        }

        if (other.CompareTag("Storm"))
        {
            player.inStorm = false;
        }
    }
}