using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    [Header("Tilemaps and DB")]
    public Tilemap tilemap = new Tilemap();
    public Tilemap physicsTilemap;
    public GarbageDatabase garbageDB = null;
    public GameObject anchor = null;
    public List<GarbageClass> spawnedObjects = null;
    public List<GameObject> cloneList = null;


    [Header("SpawnParams")]
    public int numberToSpawn;
    public int maxObjects = 150;
    public float waitBetweenSpawns = 2f;

    [Header("Invoice Prints")]
    private bool isSpawning = false;

    void Start()
    {
        
    }


    void FixedUpdate()
    {
        if (!isSpawning && Pollution.instance.isAlive) { StartCoroutine(SpawningRoutine()); }
        
    }
    private IEnumerator SpawningRoutine()
    {
        isSpawning = true;
        yield return new WaitForSeconds(waitBetweenSpawns);
        if (spawnedObjects.Count + numberToSpawn < maxObjects)
        {
            print("Spawn");
            SpawnEntity(numberToSpawn);
            yield return null;
        }
        else
        {
            print("Spawn else");
            SpawnEntity(maxObjects - spawnedObjects.Count);
            yield return null;
        }
        isSpawning = false;
        yield return null;

    }
    public void SpawnEntity(int entitiesToSpawn, myState desiredState = myState.Both)
    {
        if (spawnedObjects.Count + entitiesToSpawn <= maxObjects)
        {
            for (int i = 0; i < entitiesToSpawn; i++)
            {
                GarbageClass randomObject = garbageDB.GetAllObjects(Random.Range(0, garbageDB.garbageCount));
                Vector2 locationX = new Vector2(Random.Range(tilemap.localBounds.min.x, tilemap.localBounds.max.x), Random.Range(tilemap.localBounds.min.y, tilemap.localBounds.max.y));

                if (!physicsTilemap.GetComponent<CompositeCollider2D>().OverlapPoint(locationX))
                {
                    if (desiredState == myState.Trash)
                    {
                        List<GarbageClass> trashList = new List<GarbageClass>();
                        trashList = garbageDB.GetAllTrash();
                        randomObject = trashList[Random.Range(0, trashList.Count)];
                        print(trashList.Count);
                    }
                    var spawnedObject = Instantiate(randomObject.prefab, locationX, transform.rotation, anchor.transform);
                    spawnedObject.name = randomObject.name;
                    spawnedObjects.Add(randomObject);

                }
            }
        }
        
    }
}
