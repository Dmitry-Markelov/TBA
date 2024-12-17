using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ZoneController : MonoBehaviour
{
    private Player player;
    private Transport transport;
    [SerializeField] float zoneDamage = 0.1f;
    [SerializeField] float folowSpeed = 2f;
    private bool playerInZone = false;
    private bool transportInZone = false;

    void Start()
    {
        player = FindObjectOfType<Player>();
        transport = FindObjectOfType<Transport>();
    }

    void FixedUpdate()
    {
        if (playerInZone)
        {
            player.GetDamage(zoneDamage);
        }

        if (transportInZone)
        {
            transport.GetDamage(zoneDamage);
        }

        if (player.transform.position.x < transform.position.x)
        {
            player.GetDamage(zoneDamage * 10);
        }

        if (transport.transform.position.x < transform.position.x)
        {
            transport.GetDamage(zoneDamage * 10);
        }

        if (player.transform.position.x - transform.position.x >= 250)
        {
            transform.position = new Vector3(player.transform.position.x - 100, transform.position.y, transform.position.z);
        }
        transform.position += new Vector3(folowSpeed * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInZone = true;
        }

        if (other.tag == "Transport")
        {
            transportInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInZone = false;
        }

        if (other.tag == "Transport")
        {
            transportInZone= false;
        }
    }
}