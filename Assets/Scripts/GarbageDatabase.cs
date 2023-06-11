using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu]
public class GarbageDatabase : ScriptableObject
{
    public int garbageCount
    {
        get { return garbageClass.Length; }
    }
    public GarbageClass GetAllObjects(int index)
    {
        return garbageClass[index];
    }
    public List<GarbageClass> GetAllTrash()
    {
        for(int i = 0; i < garbageCount; i++)
        {
            
            if(GetAllObjects(i).state == myState.Trash)
            {
                Debug.Log(i);
                trashList.Add(GetAllObjects(i));
                return trashList;
            }
            
        }
        return null;
    }

    public List<GarbageClass> trashList = new List<GarbageClass>();
    public GarbageClass[] garbageClass;
}
