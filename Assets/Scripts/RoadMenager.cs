using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoadMenager : MonoBehaviour
{
    public GameObject[] roadTiles;
    public float zSpawnLocation = 0;
    public float tileLength = 40;
    public int numberOfVisibleTiles = 7;
    public Transform playerTransformPosition;
    private List<GameObject> activeTiles = new List<GameObject> ();
    void Start()
    {
      for (int i = 0; i < numberOfVisibleTiles; i++)
        {
            SpawnTile(0);
        }
    }

  
    void Update()
    {
        if (playerTransformPosition.position.z > zSpawnLocation - (numberOfVisibleTiles * tileLength))
        {
            SpawnTile(0);
            DeleteTile();
        }
    }
    public void SpawnTile(int tileIndex)
    {
        Vector3 targetPosition = new Vector3(6.27255917f, -7.57999992f, 0);
        GameObject instantiatedPrefab =  Instantiate(roadTiles[tileIndex], transform.forward * zSpawnLocation, transform.rotation);
        instantiatedPrefab.transform.Translate(targetPosition);
        activeTiles.Add(instantiatedPrefab);
        zSpawnLocation += tileLength;
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
  
}
