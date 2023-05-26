using System;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    public GameObject Car;
    public GameObject[] objectsToSpawn;  // Array of objects to spawn
    public Transform[] spawnPoints;  // Array of spawn points representing lanes
    public float spawnInterval = 0.1f;  // Time interval between spawns

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    private void SpawnObject()
    {
        int randomObjectIndex = UnityEngine.Random.Range(0, objectsToSpawn.Length);
        GameObject objectToSpawn = objectsToSpawn[randomObjectIndex];
        string spawnedObjectName =objectToSpawn.name;
        if (spawnedObjectName == "ObstacleCone")
        {
            bool shouldDoubleSpawn = UnityEngine.Random.Range(0, 2) == 1;
            int randomLaneIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomLaneIndex];

            Debug.Log(Car.transform.position.z);
            spawnPoint.position += new Vector3(0f, 0f, Car.transform.position.z + 20f);
            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
