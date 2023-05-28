using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public Button Mk1Button;
    public Button Mk2Button;

    private void Start()
    {
        Mk1Button.onClick.AddListener(MK1ButtonPressed);
        Mk2Button.onClick.AddListener(MK2ButtonPressed);
    }

    void MK1ButtonPressed()
    {
        
        print("Worked1!");

    }

    void MK2ButtonPressed()
    {
        print("Worked2!");

    }
}
