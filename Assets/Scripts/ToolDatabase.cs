using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ToolDatabase : ScriptableObject
{
    public ToolClass[] toolClass;
    public int ToolCount {
        get { return toolClass.Length; }
    }
    public ToolClass GetTool(int index)
    {
        return toolClass[index];
    }
}
