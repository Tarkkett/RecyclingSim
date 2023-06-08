using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recycler : MonoBehaviour
{
    public Transform outputPoint;
    public GarbageDatabase garbageDB;
    public Movement movement;
    public Spawner spawner;
    public Transform prefabAnchor;

    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Trash")
        {
            for (int i = 0; i < spawner.spawnedObjects.Count; i++)
            {
                print(collision.gameObject.name);
                if (spawner.spawnedObjects[i].prefab == collision.gameObject)
                {
                    print(i);
                    Destroy(collision.gameObject);
                    switch (spawner.spawnedObjects[i].type)
                    {
                        case myType.Bottle:
                            InstantiateObjectByType(myType.Bottle);
                            break;
                        case myType.Jar:
                            InstantiateObjectByType(myType.Jar);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void InstantiateObjectByType(myType type)
    {
        
        /*for (int i = 0; i < garbageDB.garbageCount; i++)
        {
            if (garbageDB.GetTrash(i).type == type && garbageDB.GetTrash(i).state == myState.Recycled)
            {
                Instantiate(garbageDB.GetTrash(i).prefab, outputPoint, prefabAnchor);
            }
        }*/
    }

    /*public GarbageClass GetClassFromPrefab(GameObject obj)
    {
        GarbageClass gClass = null;
        for (int i = 0; i < spawner.spawnedObjects.Count; i++)
        {
            print(spawner.spawnedObjects[i].prefab);
            print("           " + obj);
            if (spawner.spawnedObjects[i].prefab == obj)
            {
                gClass = spawner.spawnedObjects[i];
                return gClass;
            }
        }
        return null;
    }*/

    /*private int ConvertObject(int j)
    {
        GameObject gameObject = spawner.spawnedObjects[j];
        for (int i = 0; i < garbageDB.garbageCount; i++)
        {
            if (gameObject == garbageDB.GetTrash(i).prefab)
            {
                print("Found 2");
                return i;
            }
            else
            {
                return 0;
            }
        }
        return 0;

    }*/
}
