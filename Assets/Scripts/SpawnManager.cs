using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private float spawnRate = 1.0f;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject playerSpaceCraft;
    private int lastCheckedScore = 0;

    [SerializeField]
    private GameObject heartPrefab;

    private float lastSpawnX;
    private int enemyCount = 1;

    bool halfDone = false;
    private float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!halfDone && timer >= spawnRate * 0.5f)
        {
            SpawnHeart();
            halfDone = true;
        }

        if (timer >= spawnRate)
        {
            Spawner();

            for (int i = 0; i < enemyCount; i++)
                Spawn();

            timer = 0f;
            halfDone = false; // сбрасываем флаг
        }
    }

    void Spawner()
    {
        if (playerSpaceCraft.GetComponent<PlayerController>().totalScore - lastCheckedScore >= (enemyPrefab.GetComponent<Enemy>().scoreValue * 4))
        {
            if (spawnRate >= 1.3f)
            {
                spawnRate -= 0.1f; // постепенное увелечение частоты спавна противников
                // усиление частоты огня
                playerSpaceCraft.GetComponent<PlayerController>().fireRate -= (playerSpaceCraft.GetComponent<PlayerController>().fireRate / 10f);
            }
            else if (enemyCount < 3)
                enemyCount++; // увеличение числа противников, максимум 3
            
            lastCheckedScore = playerSpaceCraft.GetComponent<PlayerController>().totalScore;
        }
    }
    void SpawnHeart()
    {
        if (Random.Range(0, 10) != 5)
            return;

        Vector3 spawnPos = new Vector3(Random.Range(-30f, 30f), 11.5f, 75f);
        Instantiate(heartPrefab, spawnPos, heartPrefab.transform.rotation);
    }
    void Spawn()
    {
        float newX;

        // генерируем пока не будет достаточно далеко
        do
        {
            newX = Random.Range(-30f, 30f);
        }
        while (Mathf.Abs(newX - lastSpawnX) < 6f);

        Vector3 spawnPos = new Vector3(newX, 11.5f, 75f);

        Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);

        lastSpawnX = newX;
    }
}
