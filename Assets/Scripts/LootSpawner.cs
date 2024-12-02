using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public GameObject[] lootPrefabs;
    public Transform player;
    public float spawnDistance = 50f;
    public float spawnInterval = 10f;

    private float spawnTimer;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnLoot();
            spawnTimer = 0f;
        }
    }

    void SpawnLoot()
    {
        int randomIndex = Random.Range(0, lootPrefabs.Length);
        Vector3 spawnPosition = new Vector3(player.position.x + Random.Range(20f, 50f), 5.2f, 0);
        Instantiate(lootPrefabs[randomIndex], spawnPosition, Quaternion.identity);
    }
}