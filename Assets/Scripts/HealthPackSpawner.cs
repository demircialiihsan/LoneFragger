using System.Collections;
using UnityEngine;

public class HealthPackSpawner : MonoBehaviour
{
    public GameObject healthPackPrefab;

    public Transform spawnPointsParent;
    private Transform[] spawnPoints;

    private Camera cam;

    [HideInInspector]
    public bool gameEnded = false;

    public float spawnRate = 5f;

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
        while (!gameEnded)
        {
            SpawnHealthPack();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnHealthPack(int take = 0)
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
            SpawnHealthPack(++take);
        }
        else
        {
            GameObject hp = Instantiate(healthPackPrefab, spawnPoints[r].position, Quaternion.identity);
            Destroy(hp, 40);
        }
    }
}
