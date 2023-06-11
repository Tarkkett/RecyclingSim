using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class GarbageClass
{
    public string name;
    public GameObject prefab;
    public int value;
    public myType type;
    public myState state;
}
public enum myType
{
    Bottle_Water,
    Jar,
    Bottle_Wine
}
public enum myState
{
    Trash,
    Recycled,
    Both
}
