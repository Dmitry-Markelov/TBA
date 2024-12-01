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
        Vector3 spawnPosition = player.position + new Vector3(Random.Range(20f, 50f), 0, 0);
        int randomIndex = Random.Range(0, lootPrefabs.Length);
        Instantiate(lootPrefabs[randomIndex], spawnPosition, Quaternion.identity);
    }
}