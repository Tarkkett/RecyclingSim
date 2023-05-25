using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDelete : MonoBehaviour
{

    int timer = 2;
    void Start()
    {
        Destroy(gameObject, timer);
    }


}
