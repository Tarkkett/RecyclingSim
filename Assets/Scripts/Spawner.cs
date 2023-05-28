using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    public Tilemap tilemap = new Tilemap();
    public GameObject[] gameObjects = null;
    public int numberToSpawn;
    
    public int maxObjects = 150;
    [SerializeField]private int objInScene;
    public int waitBetweenSpawns = 2;

    private bool isSpawning = false;


    public List<GameObject> spawnedObjects = null;

    void Start()
    {

        
        
    }

    void Update()
    {
        objInScene = spawnedObjects.Count;
        if (!isSpawning) { StartCoroutine(SpawningRoutine()); }
        
    }
    private IEnumerator SpawningRoutine()
    {
        isSpawning = true;
        if (spawnedObjects.Count <= maxObjects)
        {
            yield return new WaitForSeconds(waitBetweenSpawns);
            for (int i = 0; i < numberToSpawn; i++)
            {
                var trash = Instantiate(gameObjects[Random.Range(0, gameObjects.Length)], new Vector3(Random.Range(tilemap.localBounds.min.x, tilemap.localBounds.max.x), Random.Range(tilemap.localBounds.min.y, tilemap.localBounds.max.y), 0), transform.rotation);
                spawnedObjects.Add(trash);
            }
            yield return null;
        }
        isSpawning = false;
        yield return null;

    }
}
