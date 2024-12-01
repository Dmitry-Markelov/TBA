using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Enter enter;
    private LootObject currentLootObject;

    private void Awake()
    {
        enter = FindAnyObjectByType<Enter>();
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
        if (other.CompareTag("Loot"))
        {
            currentLootObject = other.GetComponent<LootObject>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Loot"))
        {
            currentLootObject = null;
        }
    }
}