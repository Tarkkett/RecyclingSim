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
    [SerializeField]private int objInScene;
    public int waitBetweenSpawns = 2;

    [Header("Invoice Prints")]
    private bool isSpawning = false;

    void Start()
    {
        
    }


    void FixedUpdate()
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
                SpawnEntity();
            }
            yield return null;
        }
        isSpawning = false;
        yield return null;

    }
    private void SpawnEntity()
    {

        GarbageClass randomObject = garbageDB.GetTrash(Random.Range(0, garbageDB.garbageCount));
        Vector2 locationX = new Vector2(Random.Range(tilemap.localBounds.min.x, tilemap.localBounds.max.x), Random.Range(tilemap.localBounds.min.y, tilemap.localBounds.max.y));

        if (!physicsTilemap.GetComponent<CompositeCollider2D>().OverlapPoint(locationX)) {
            
           var spawnedObject = Instantiate(randomObject.prefab, locationX, transform.rotation, anchor.transform);
            spawnedObjects.Add(randomObject);
            cloneList.Add(spawnedObject);

        }
        
    }
}
