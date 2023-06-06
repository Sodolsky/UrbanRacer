using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 25;
    public int numberOfTiles = 5;
    public Transform playerTransform;
    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        for (int i = 0; i < numberOfTiles; i++)
        {
            if(i==0)
            {
                SpawnTile(0);
            } else
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
            }
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z -35 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }
    public void SpawnTile(int tileIndex)
    {
        GameObject roadTile =Instantiate(tilePrefabs[tileIndex], transform.forward*zSpawn, transform.rotation);
        roadTile.transform.Translate(6.27255917f, -7.57999992f,0);
        activeTiles.Add(roadTile);
        zSpawn += tileLength;
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);

    }
}
