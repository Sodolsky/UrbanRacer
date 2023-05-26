using System;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    public GameObject Car;
    public GameObject[] objectsToSpawn;  // Array of objects to spawn
    public Transform[] spawnPoints;  // Array of spawn points representing lanes
    public float spawnInterval = 0.8f;  // Time interval between spawns
    private int lastSpawnedLaneIndex = -1;  

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
        int randomNumber = UnityEngine.Random.Range(0, 100);
        /*
         Object Indexes in objectsToSpawnArray
        0-Cone
        1-Barrier
         */
        GameObject objectToSpawn;

        if (randomNumber <= 80) {
            objectToSpawn = objectsToSpawn[0];
        }
        else
        {
            objectToSpawn = objectsToSpawn[1];
        }
        string spawnedObjectName = objectToSpawn.name;

        if (spawnedObjectName == "ObstacleCone")
        {
            bool shouldDoubleSpawn = UnityEngine.Random.Range(0, 2) == 1;
            if (shouldDoubleSpawn)
            {
                SpawnOnTwoLanes(objectToSpawn);
            }
            else
            {
                SpawnOnSingleLane(objectToSpawn);
            }
        }else if(spawnedObjectName == "ObstacleBarrier")
        {
            SpawnOnMiddleLane(objectToSpawn);
        }
    }
    private void SpawnOnSingleLane(GameObject objectToSpawn)
    {
        int randomLaneIndex = GetRandomLaneIndex();
        Transform spawnPoint = spawnPoints[randomLaneIndex];

        spawnPoint.position += new Vector3(0f, 0f, Car.transform.position.z + 15f);
        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);

        lastSpawnedLaneIndex = randomLaneIndex;
    }
    private void SpawnOnMiddleLane(GameObject objectToSpawn)
    {
        Transform spawnPoint = spawnPoints[1];
        spawnPoint.position += new Vector3(0f, 0f, Car.transform.position.z + 15f);

        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        spawnedObject.transform.Translate(-4.3f, 0f, 0f);
        spawnedObject.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
    }

    private void SpawnOnTwoLanes(GameObject objectToSpawn)
    {
        int randomLaneIndex1 = GetRandomLaneIndex();
        int randomLaneIndex2;
        do
        {
            randomLaneIndex2 = GetRandomLaneIndex();
        } while (randomLaneIndex2 == randomLaneIndex1);

        Transform spawnPoint1 = spawnPoints[randomLaneIndex1];
        Transform spawnPoint2 = spawnPoints[randomLaneIndex2];

        spawnPoint1.position += new Vector3(0f, 0f, Car.transform.position.z + 15f);
        Instantiate(objectToSpawn, spawnPoint1.position, spawnPoint1.rotation);

        spawnPoint2.position += new Vector3(0f, 0f, Car.transform.position.z + 15f);
        Instantiate(objectToSpawn, spawnPoint2.position, spawnPoint2.rotation);

        lastSpawnedLaneIndex = randomLaneIndex1;
    }

    private int GetRandomLaneIndex()
    {
        int randomLaneIndex;
        do
        {
            randomLaneIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        } while (randomLaneIndex == lastSpawnedLaneIndex);  // Ensure it's not the same as last spawned lane

        return randomLaneIndex;
    }
}
