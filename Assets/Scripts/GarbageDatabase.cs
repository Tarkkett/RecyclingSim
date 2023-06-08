using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GarbageDatabase : ScriptableObject
{
    public int garbageCount
    {
        get { return garbageClass.Length; }
    }
    public GarbageClass GetTrash(int index)
    {
        return garbageClass[index];
    }
    public GarbageClass[] garbageClass;
}
