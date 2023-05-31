using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    public Tilemap tilemap = new Tilemap();
    public Tilemap physicsTilemap;
    public GameObject[] gameObjects = null;
    public int numberToSpawn;
    
    public int maxObjects = 150;
    [SerializeField]private int objInScene;
    public int waitBetweenSpawns = 2;

    private bool isSpawning = false;

    public GameObject anchor = null;


    public List<GameObject> spawnedObjects = null;

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
        GameObject RandomObject = gameObjects[Random.Range(0, gameObjects.Length)];
        Vector2 locationX = new Vector2(Random.Range(tilemap.localBounds.min.x, tilemap.localBounds.max.x), Random.Range(tilemap.localBounds.min.y, tilemap.localBounds.max.y));

        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(locationX.x, locationX.y), Vector2.zero, 100f);
        Debug.DrawRay(new Vector3(0, 0, 0), Vector2.zero, Color.magenta);

        if (!physicsTilemap.GetComponent<CompositeCollider2D>().OverlapPoint(locationX)) {
            
            var trash = Instantiate(RandomObject, locationX, transform.rotation, anchor.transform);
            spawnedObjects.Add(trash);
        }
        else
        {

        }
        

        

        /*foreach (RaycastHit2D hit in hits) {

            print(hit.collider.gameObject.name);
            bool overlaps = physicsTilemap.OverlapPoint(hit.point);
            if (overlaps) {
                print("Does Work");
                print(hit.collider.gameObject.name);
            }

            /*if (hit.collider.composite != null)
            {
                if (hit.collider.composite.gameObject.name == "PhysicalObjects")
                {
                    print(hit.collider.gameObject.name);
                    print("Collision finally!");
                }
                
                
                
            }
            else
            {
                print(hit.collider.name);
                print("null");
            }
            
        
        }*/

        
        
        
    }
}
