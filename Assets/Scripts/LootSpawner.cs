using UnityEngine;

public class LootSpawner : MonoBehaviour
{

    public GameObject[] lootPrefabs;
    public Transform player;
    public float minSpawnDistance = 100f;
    public float spawnInterval = 10f;

    private float spawnTimer;
    private Vector3 lastSpawnPos;

    private void Start()
    {
        lastSpawnPos = player.position;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && Vector3.Distance(player.position, lastSpawnPos) >= minSpawnDistance)
        {
            SpawnLoot();
            spawnTimer = 0f;
        }
    }

    void SpawnLoot()
    {
        int randomIndex = Random.Range(0, lootPrefabs.Length);

        Vector3 spawnPos = new Vector3(
            player.position.x + Random.Range(20f, 50f), 
            5.2f, 
            0);

        Instantiate(lootPrefabs[randomIndex], spawnPos, Quaternion.identity);

        lastSpawnPos = spawnPos;
    }
}