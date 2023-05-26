using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    public Tilemap tilemap = new Tilemap();
    public GameObject[] gameObjects = null;
    public int numberToSpawn;
    bool spawning = true;
    
    // Start is called before the first frame update
    void Start()
    {
        

        for (int i = 0; i < numberToSpawn; i++)
        {
            Instantiate(gameObjects[Random.Range(0, gameObjects.Length)], new Vector3(Random.Range(tilemap.localBounds.min.x, tilemap.localBounds.max.x), Random.Range(tilemap.localBounds.min.y, tilemap.localBounds.max.y), 0), transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
