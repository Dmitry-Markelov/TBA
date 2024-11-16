using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    private Player player;
    [SerializeField] float zoneDamage = 0.1f;
    [SerializeField] float folowSpeed = 2f;
    private bool inZone = false;


    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        if (inZone)
        {
            player.GetDamage(zoneDamage);
        }

        if (player.transform.position.x - transform.position.x >= 100)
        {
            transform.position = new Vector3(player.transform.position.x - 40, transform.position.y, transform.position.z);
        }
        transform.position += new Vector3(folowSpeed * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inZone = false;
        }
    }
}