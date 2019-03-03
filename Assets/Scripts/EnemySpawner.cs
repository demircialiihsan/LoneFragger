using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnRate = 2f;
    public float timeBeforeFirstSpawn = 5f;

    public Transform spawnPointsParent;
    private Transform[] spawnPoints;

    private Camera cam;

    private bool gameEnded = false;
    public static List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        spawnPoints = new Transform[spawnPointsParent.childCount];

        for (int i = 0; i < spawnPointsParent.childCount; i++)
        {
            spawnPoints[i] = spawnPointsParent.GetChild(i);
        }

        cam = Camera.main;

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timeBeforeFirstSpawn);

        while (!gameEnded)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnEnemy(int take = 0)
    {
        if (take >= 100)
        {
            Debug.Log("Failed to find a spawnpoint outside the visible area");
            return;
        }

        int r = Random.Range(0, spawnPoints.Length);

        Vector3 position = cam.WorldToViewportPoint(spawnPoints[r].position);

        if (position.x > 0 && position.x < 1 && position.y > 0 && position.y < 1)
        {
            SpawnEnemy();
        }
        else
        {
            enemies.Add(Instantiate(enemyPrefab, spawnPoints[r].position, Quaternion.identity));
        }
    }

    public void DisableEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Enemy>().enabled = false;
        }
        gameEnded = true;
    }
}
