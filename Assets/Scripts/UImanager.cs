using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private int score;
    public Movement movement;
    public TextMeshProUGUI garbageRemovedText;

    private void Start()
    {
        score = 0;
        

    }
    private void Update()
    {
        if (score < 10)
        {
            garbageRemovedText.text = "0000" + score.ToString();
        }
        else if (score < 100) { garbageRemovedText.text = "0000" + score.ToString(); }
        else if (score < 1000) { garbageRemovedText.text = "000" + score.ToString(); }
        else if (score < 10000) { garbageRemovedText.text = "00" + score.ToString(); }
        else if (score < 100000) { garbageRemovedText.text = "0" + score.ToString(); }
        else if (score < 1000000) { garbageRemovedText.text = "" + score.ToString(); }
        else if (score < 10000000) { garbageRemovedText.text = (score/1000000).ToString() +"M"; }
        score = movement.garbageRemoved;
        
    }
}
