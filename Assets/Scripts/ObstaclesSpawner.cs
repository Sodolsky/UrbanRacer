using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    public GameObject Car;
    public GameObject[] objectsToSpawn;  // Array of objects to spawn
    public Transform[] spawnPoints;  // Array of spawn points representing lanes
    public float spawnInterval = 0.8f;  // Time interval between spawns
    public float spawnDistanceFromPlayer;
    private float timer = 0f;
    public enum MultiLaneObjects
    {
        ObstacleCone,
        ObstacleRoadBlocker,
        ObstacleWaterBarrel,
        ObstacleRedBarrier,
        ObstacleStop
    }
    private void Start()
    {
        if (!PlayerManager.isGameStarted)
        {
            AlterSpawnPoints(true);
        }
    }
    //TODO Fix lane spawn points moving away!
    private void Update()
    {
        if (!PlayerManager.isGameStarted) return;
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
        2-RoadBlocker
        3-WaterBarrel
        4-RedBarrier
        5-ObstacleStop
         */
        GameObject objectToSpawn;

        if (randomNumber <= 15) {
            objectToSpawn = objectsToSpawn[0];
        }
        else if (randomNumber>15&&randomNumber<=35)
        {
            objectToSpawn = objectsToSpawn[2];
        }  else if (randomNumber>35&&randomNumber<=55)
        {
            objectToSpawn = objectsToSpawn[3];
        } else if (randomNumber>55&&randomNumber<=75)
        {
            objectToSpawn = objectsToSpawn[4];
        }else if (randomNumber>75&&randomNumber<=90)
        {
            objectToSpawn = objectsToSpawn[5];
        }
        else
        {
            objectToSpawn = objectsToSpawn[1];
        }
        string spawnedObjectName = objectToSpawn.name;

        if (Enum.IsDefined(typeof(MultiLaneObjects), spawnedObjectName))
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
        Debug.Log(objectToSpawn.transform.rotation.x);
        int randomLaneIndex = GetRandomLaneIndex();
        Transform spawnPoint = spawnPoints[randomLaneIndex];

        spawnPoint.position = new Vector3(spawnPoints[randomLaneIndex].transform.position.x, 0f, Car.transform.position.z + spawnDistanceFromPlayer);
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        if (spawnedObject.name == "ObstacleRoadBlocker(Clone)") spawnedObject.transform.rotation = Quaternion.Euler(-90f, 0, 0);
        if (spawnedObject.name == "ObstacleStop(Clone)") spawnedObject.transform.rotation = Quaternion.Euler(0, -90f, 0);
        }
        private void SpawnOnMiddleLane(GameObject objectToSpawn)
    {
        Transform spawnPoint = spawnPoints[1];
        spawnPoint.position = new Vector3(spawnPoints[1].transform.position.x, 0f, Car.transform.position.z + spawnDistanceFromPlayer+5f);

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

        spawnPoint1.position = new Vector3(spawnPoints[randomLaneIndex1].transform.position.x, 0f, Car.transform.position.z + spawnDistanceFromPlayer);
        GameObject spawnedObject;
        spawnedObject = Instantiate(objectToSpawn, spawnPoint1.position, spawnPoint1.rotation);
        if (spawnedObject.name == "ObstacleRoadBlocker(Clone)") spawnedObject.transform.rotation = Quaternion.Euler(-90f, 0, 0);
        if (spawnedObject.name == "ObstacleStop(Clone)") spawnedObject.transform.rotation = Quaternion.Euler(0, -90f, 0);
        spawnPoint2.position = new Vector3(spawnPoints[randomLaneIndex2].transform.position.x, 0f, Car.transform.position.z + spawnDistanceFromPlayer);
        spawnedObject=Instantiate(objectToSpawn, spawnPoint2.position, spawnPoint2.rotation);
        if (spawnedObject.name == "ObstacleRoadBlocker(Clone)") spawnedObject.transform.rotation = Quaternion.Euler(-90f, 0, 0);
        if (spawnedObject.name == "ObstacleStop(Clone)") spawnedObject.transform.rotation = Quaternion.Euler(0, -90f, 0);

    }

    private int GetRandomLaneIndex()
    {
        int randomLaneIndex;
            randomLaneIndex = UnityEngine.Random.Range(0, spawnPoints.Length);

        return randomLaneIndex;
    }
    private void AlterSpawnPoints(Boolean fullReset)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Vector3 newPosition = spawnPoints[i].position;
            newPosition.z = fullReset ? 0f : Car.transform.position.z+30f;
            spawnPoints[i].position = newPosition;
        }
    } 
}
