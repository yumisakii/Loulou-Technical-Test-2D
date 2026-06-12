using UnityEngine;
using System.Collections.Generic;

public class BalloonSpawner : MonoBehaviour
{
    [Header("Dependencies")]
    public LevelData currentLevel;
    public GameObject balloonPrefab;

    [Header("Spawn Area")]
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;

    private float timer;

    void Update()
    {
        if (currentLevel == null) return;

        timer += Time.deltaTime;
        if (timer >= currentLevel.spawnRate)
        {
            SpawnBalloon();
            timer = 0;
        }
    }

    void SpawnBalloon()
    {
        float randomX = Random.Range(leftSpawnPoint.position.x, rightSpawnPoint.position.x);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y, 0);

        GameObject newBalloon = Instantiate(balloonPrefab, spawnPos, Quaternion.identity);

        bool isCorrect = Random.value > 0.5f;
        List<BalloonData> pool = isCorrect ? currentLevel.correctBalloons : currentLevel.incorrectBalloons;

        if (pool == null || pool.Count == 0) return;

        BalloonData selectedData = pool[Random.Range(0, pool.Count)];

        newBalloon.GetComponent<Balloon>().Initialize(selectedData, currentLevel, isCorrect);
    }
}