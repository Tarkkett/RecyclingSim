using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ToolClass
{
    public GameObject toolPrefab;
    public Sprite toolSprite;
    public string toolName;
    public bool isRepulsive;
    public int repulsionStrength;
}
